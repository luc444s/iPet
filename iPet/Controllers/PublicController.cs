using iPet.Models;
using iPet.Models.AjaxSerialization;
using iPet.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace iPet.Controllers
{
    [RedirectingActionAttribute]
    public class PublicController : Controller
    {
        private SiteContext db = new SiteContext();

        // GET: Public
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Favorites()
        {
            var userID = Convert.ToInt32(Session["UserID"]);

            List<FavoritePet> petsfavorited = db.FavoritePet.Where(p => p.UserID == userID).ToList();
            List<int> petsIDS = new List<int>();

            foreach (var p in petsfavorited)
            {
                petsIDS.Add(p.PetID);
            }

            List<Pet> pets = db.Pets.Where(p => petsIDS.Contains(p.ID)).ToList();            

            return View(pets.ToList());
        }

        [HttpPost]
        public ActionResult Find(PetFinder petData)
        {
            var userID = Convert.ToInt32(Session["UserID"]);

            System.Diagnostics.Debug.WriteLine($"raca -> {petData.Raca}, porte -> {petData.Porte}, cor -> {petData.Cor}, preco -> {petData.Preco}");

            List<Pet> pets = db.Pets.ToList();

            foreach (var p in pets)
            {
                p.GetMatch(petData.Raca, petData.Porte, petData.Sexo, petData.Cor, petData.Preco);
            }

            pets = pets.OrderByDescending(p => p.MatchPercent).ToList();

            return View(pets);
        }

        // GET: Pets/Details/5
        public ActionResult Details(int id)
        {
            var userID = Convert.ToInt32(Session["UserID"]);

            Pet pet = db.Pets.Find(id);

            List<FavoritePet> petsfavorited = db.FavoritePet.Where(p => p.UserID == userID).ToList();
            List<int> petsIDS = new List<int>();

            foreach (var p in petsfavorited)
            {
                petsIDS.Add(p.PetID);
            }

            List<Pet> pets = db.Pets.Where(p => petsIDS.Contains(p.ID)).ToList();
            List<int> petsFavoritedIDS = new List<int>();

            foreach (var p in pets)
            {
                petsFavoritedIDS.Add(p.ID);
            }

            ViewBag.isFavorite = false;

            if (petsFavoritedIDS.Contains(id)) {
                ViewBag.isFavorite = true;
            }

            if (pet == null)
            {
                return HttpNotFound();
            }

            return View(pet);
        }
    }
}