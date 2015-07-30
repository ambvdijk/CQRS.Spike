using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using Autofac;
using Autofac.Integration.Mvc;
using CQRS.Spike.Core;
using CQRS.Spike.Core.Configuration;
using CQRS.Spike.Core.Serialization;
using CQRS.Spike.Domain.CommandHandlers;
using CQRS.Spike.Infra.SQL;

namespace CQRS.Spike.Web.UI
{
  public class Global : HttpApplication
  {
    private void Application_Start(object sender, EventArgs e)
    {
      ConfigureMvc();
      ConfigureWebApi();
    }

    #region -- MVC Configuration --

    private static void ConfigureMvc()
    {
      AreaRegistration.RegisterAllAreas();
      ConfigureMvcRoutes(RouteTable.Routes);
      ConfigureDependencies();
    }

    private static void ConfigureDependencies()
    {
      var builder = new ContainerBuilder();

      builder
        .RegisterType<CreateCompanyCommandHandler>()
        .As<ICommandHandler>();

      builder
        .RegisterType<JsonTextSerializer>()
        .As<ITextSerializer>()
        .SingleInstance();

      builder
        .RegisterType<ConnectionConfiguration>()
        .As<IConnectionConfiguration>()
        .SingleInstance();

      builder
        .RegisterType<SqlEventStore>()
        .WithParameter("connectionName","EventStore")
        .As<IEventStore>()
        .SingleInstance();

      builder
        .RegisterType<EventDispatcher>()
        .AsImplementedInterfaces()
        .SingleInstance();

      builder
        .RegisterType<SyncEventBus>()
        .As<IEventBus>()
        .SingleInstance();

      builder
        .RegisterType<CommandDispatcher>()
        .AsImplementedInterfaces()
        .SingleInstance();

      builder
        .RegisterType<SyncCommandBus>()
        .As<ICommandBus>()
        .SingleInstance();

      builder
        .RegisterType<CommandRegistrator>()
        .AutoActivate();

      // Register your MVC controllers.
      builder.RegisterControllers(typeof(Global).Assembly);

      //OPTIONAL: Register model binders that require DI.
      //builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
      //builder.RegisterModelBinderProvider();

      //OPTIONAL: Register web abstractions like HttpContextBase.
      //builder.RegisterModule<AutofacWebTypesModule>();

      // OPTIONAL: Enable property injection in view pages.
      //builder.RegisterSource(new ViewRegistrationSource());

      // OPTIONAL: Enable property injection into action filters.
      //builder.RegisterFilterProvider();

      // Set the dependency resolver to be Autofac.

      DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));
    }

    private static void ConfigureMvcRoutes(RouteCollection routes)
    {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

      routes.MapRoute(
        name: "Default",
        url: "{controller}/{action}/{id}",
        defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional}
        );
    }

    #endregion

    #region -- Web Api Configuration --

    private void ConfigureWebApi()
    {
      GlobalConfiguration.Configure(ConfigureWebApi);
    }

    private static void ConfigureWebApi(HttpConfiguration config)
    {
      config.MapHttpAttributeRoutes();

      config.Routes.MapHttpRoute(
        name: "DefaultApi",
        routeTemplate: "api/{controller}/{id}",
        defaults: new {id = RouteParameter.Optional}
        );
    }

    #endregion
  }
}