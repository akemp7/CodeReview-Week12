using Microsoft.AspNetCore.Mvc;
using Bakery.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Bakery.Controllers
{
     public class TreatsController : Controller
  {
    private readonly BakeryContext _db;

    public TreatsController(BakeryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Treat> model = _db.Treats.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Description");
      return View();
    }

    [HttpPost]
        public ActionResult Create(Treat treat, int FlavorId)
        {
            _db.Treats.Add(treat);
            if(FlavorId != 0)
            {
                _db.TreatFlavor.Add(new TreatFlavor(){FlavorId = FlavorId, TreatId= treat.TreatId});
            }
            _db.SaveChanges();
            return RedirectToAction("Index", "Account");
        }
      
      public ActionResult Details(int id)
        {
            Treat treat = _db.Treats
                .Include(a => a.Flavors)
                .ThenInclude(join => join.Flavor)
                .FirstOrDefault(b => b.TreatId == id);
            return View(treat);
        }

  }
}