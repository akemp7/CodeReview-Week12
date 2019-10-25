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
    [Authorize]
    public class FlavorsController: Controller
    {
        private readonly BakeryContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public FlavorsController(UserManager<ApplicationUser> userManager, BakeryContext db )
        {
            _userManager= userManager;
            _db = db;
        }

        public ActionResult Index(int flavorId)
        {
            Flavor flavor = _db.Flavors
            .Include(f => f.Treats)
            .ThenInclude(join => join.Treat)
            .FirstOrDefault(d => d.FlavorId == flavorId);
            return View(flavor);
        }

         public ActionResult Create()
        {
            ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Description");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Flavor flavor, int TreatId)
        {
            _db.Flavors.Add(flavor);
            if(TreatId != 0)
            {
                _db.TreatFlavor.Add(new TreatFlavor(){TreatId = TreatId, FlavorId = flavor.FlavorId});
            }
            _db.SaveChanges();
            return RedirectToAction("Index", "Account");
        }

        public ActionResult Edit(int id)
        {
            ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Description");
            Flavor flavor = _db.Flavors
            .Include(t => t.Treats)
            .ThenInclude(join => join.Treat)
            .FirstOrDefault(f => f.FlavorId == id);
            return View(flavor);
        }

        public ActionResult Delete(int id)
        {
            Flavor flavor = _db.Flavors
            .FirstOrDefault(f => f.FlavorId == id);
            return View(flavor);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Flavor flavor = _db.Flavors.FirstOrDefault(f => f.FlavorId == id);
            _db.Flavors.Remove(flavor);
            _db.SaveChanges();
            return RedirectToAction("Index", "Account");
        }

    }
}