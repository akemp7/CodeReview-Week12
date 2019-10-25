
using Microsoft.AspNetCore.Mvc;

namespace Bakery.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      return View();
    }
  }
}