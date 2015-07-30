using System.Web.Mvc;
using AutoMapper;
using CQRS.Spike.Core;
using CQRS.Spike.Domain.Commands;
using CQRS.Spike.Web.UI.Extensions;
using CQRS.Spike.Web.UI.Models;

namespace CQRS.Spike.Web.UI.Controllers
{
  public class CompanyController : Controller
  {
    static CompanyController()
    {
      Mapper.CreateMap<CompanyViewModel, CreateCompany>();
      Mapper.AssertConfigurationIsValid();
    }

    private readonly ICommandBus _commandBus;

    public CompanyController(ICommandBus commandBus)
    {
      _commandBus = commandBus;
    }

    public ActionResult Index()
    {
      return View();
    }

    public ActionResult Create()
    {
      var viewModel = new CompanyViewModel();

      if (Request.IsHttpGet() || !TryUpdateModel(viewModel))
      {
        return View(viewModel);
      }

      _commandBus.Send(Mapper.Map<CompanyViewModel,CreateCompany>(viewModel));

      return RedirectToAction("List");
    }
  }
}