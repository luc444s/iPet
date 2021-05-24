using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using iPet.Models;
using iPet.Utils;

namespace iPet.Controllers
{
    [RedirectingActionAttribute]
    public class PetsController : Controller
    {
        private SiteContext db = new SiteContext();
        string icon = "fa fa-paw";

        // GET: Pets
        public ActionResult Index()
        {
            int userid = Convert.ToInt32(Session["UserID"]);
            ViewBag.Icon = icon;

            var pets = db.Pets.Include(p => p.User).Where(p => p.UserID == userid);
            return View(pets.ToList());
        }

        // GET: Pets/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.Icon = icon;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Pet pet = db.Pets.Find(id);

            if (pet == null)
            {
                return HttpNotFound();
            }

            return View(pet);
        }

        // GET: Pets/Create
        public ActionResult Create()
        {
            ViewBag.Icon = icon;
            return View();
        }

        // POST: Pets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pet pet)
        {
            ViewBag.Icon = icon;

            if (ModelState.IsValid)
            {
                pet.UserID = Convert.ToInt32(Session["UserID"]);                

                if (pet.PetImageFile == null)
                {
                    pet.PetImage = "default-pet.png";
                }
                else
                {
                    string fileName = pet.PetImageFile.FileName;

                    string path = Path.Combine(Server.MapPath("~/img/Pets/"),
                                           Path.GetFileName(fileName));

                    pet.PetImageFile.SaveAs(path);
                    pet.PetImage = fileName;
                }

                db.Pets.Add(pet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pet);
        }

        // GET: Pets/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Icon = icon;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pet pet = db.Pets.Find(id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "ID", "Nome", pet.UserID);
            return View(pet);
        }

        // POST: Pets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pet pet)
        {
            ViewBag.Icon = icon;

            pet.UserID = Convert.ToInt32(Session["UserID"]);

            Pet p = db.Pets.Find(pet.ID);

            if (pet.PetImageFile != null)
            {
                string fileName = pet.PetImageFile.FileName;

                string path = Path.Combine(Server.MapPath("~/img/Pets/"),
                                       Path.GetFileName(fileName));

                pet.PetImageFile.SaveAs(path);
                pet.PetImage = fileName;
            }

            if (ModelState.IsValid)
            {
                p.Nome = pet.Nome;
                p.Porte = pet.Porte;
                p.Preco = pet.Preco;
                p.Raca = pet.Raca;
                p.Vacinado = pet.Vacinado;
                p.Cor = pet.Cor;
                p.Castrado = pet.Castrado;
                p.Description = pet.Description;

                p.Sexo = pet.Sexo;

                if (pet.PetImageFile != null) {
                    p.PetImage = pet.PetImage;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "ID", "Nome", pet.UserID);
            return View(pet);
        }

        [HttpPost]
        public JsonResult FavoritarDesfavoritar(int userID, int petID)
        {
            try
            {
                FavoritePet _fpID = db.FavoritePet.Where(fp => fp.PetID == petID && fp.UserID == userID).SingleOrDefault();

                if (_fpID != null)
                {                    
                    db.FavoritePet.Remove(_fpID);

                    db.SaveChanges();

                    return Json(
                        new
                        {
                            success = true
                        }
                    );
                }
                else
                {
                    FavoritePet fp = new FavoritePet
                    {
                        PetID = petID,
                        UserID = userID
                    };

                    db.FavoritePet.Add(fp);

                    db.SaveChanges();

                    return Json(
                        new
                        {
                            success = true
                        }
                    );
                }                
            }
            catch (Exception e)
            {
                return Json(
                    new
                    {
                        success = false
                    }
                );
            }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                Pet pet = db.Pets.Find(id);

                if (pet == null)
                {
                    throw new Exception("Falha na solicitação");
                }

                db.Pets.Remove(pet);
                db.SaveChanges();
                return Json(
                    new
                    {
                        success = true,
                        message = "Pet deletado com sucesso."
                    }
                );
            }
            catch (Exception e)
            {
                return Json(
                    new
                    {
                        success = false,
                        message = e.Message
                    }
                );
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
