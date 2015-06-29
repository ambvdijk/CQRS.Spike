using System.Web.Mvc;

namespace CQRS.Spike.Web.UI.Controllers
{
  public class TabController : Controller
  {
    public ActionResult Index()
    {
      return View();
    }

    public ActionResult Create()
    {
      return View();
    }
  }
}