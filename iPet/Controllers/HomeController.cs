using iPet.Models;
using iPet.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iPet.Controllers
{
    [RedirectingActionAttribute]
    public class HomeController : Controller
    {
        private SiteContext db = new SiteContext();

        public ActionResult Index()
        {
            int userid = Convert.ToInt32(Session["UserID"]);
            List<Pet> pets = db.Pets.Where(p => p.UserID == userid).ToList();
            List<int> petsIDS = new List<int>();

            ViewBag.PetsTot = pets.Count();

            foreach (var p in pets)
            {
                petsIDS.Add(p.ID);
            }

            ViewBag.PetsFavTot = db.FavoritePet.Where(fp => petsIDS.Contains(fp.PetID)).ToList().Count();

            return View();
        }
    }
}