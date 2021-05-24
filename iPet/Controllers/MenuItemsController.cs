using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using iPet.Models;
using iPet.Models.Proc;

namespace BeautyColor_V3.Controllers
{
    public class MenuItemsController : Controller
    {
        private SiteContext db = new SiteContext();

        // GET: MenuItems
        public ActionResult Menu()
        {
            try
            {
                //Get User permission
                SqlParameter[] parameters;
                List<SP_MenuPermissions> MenuPermissions;

                string query = "SP_GETUSERMENU @Id";

                parameters = new SqlParameter[]
                {
                    new SqlParameter { ParameterName = "@Id",  Value = Session["UserID"].ToString()}
                };

                MenuPermissions = db.Database.SqlQuery<SP_MenuPermissions>(query, parameters).ToList();

                List<Menu> menu = SP_MenuPermissions.CreateMenu(MenuPermissions);
                string MenuRendered = "";

                foreach (var _m in menu)
                {
                    MenuRendered += _m.Print();
                }

                ViewBag.Menu = MenuRendered;

                return PartialView();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(string.Format("------------{0}", ex.Message));
                return PartialView("Erros", "Error404");
            }
        }

        // GET: MenuItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MenuItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MenuItem menuItem)
        {
            ModelState.Remove("DtCadastro");

            if (ModelState.IsValid)
            {
                db.MenuItems.Add(menuItem);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            return View(menuItem);
        }
    }
}
