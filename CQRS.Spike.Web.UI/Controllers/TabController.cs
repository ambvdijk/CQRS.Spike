using System.Web.Mvc;
using AutoMapper;
using CQRS.Spike.Core;
using CQRS.Spike.Domain.Commands;
using CQRS.Spike.Web.UI.Extensions;
using CQRS.Spike.Web.UI.Models;

namespace CQRS.Spike.Web.UI.Controllers
{
  public class TabController : Controller
  {
    static TabController()
    {
      Mapper.CreateMap<TabViewModel, OpenTab>();
    }

    private readonly ICommandBus _commandBus;

    public TabController(ICommandBus commandBus)
    {
      _commandBus = commandBus;
    }

    public ActionResult Index()
    {
      return View();
    }

    public ActionResult Create()
    {
      var viewModel = new TabViewModel();

      if (Request.IsHttpGet() || !TryUpdateModel(viewModel))
      {
        return View(viewModel);
      }

      _commandBus.Send(Mapper.Map<TabViewModel,OpenTab>(viewModel));

      return RedirectToAction("List");
    }
  }
}