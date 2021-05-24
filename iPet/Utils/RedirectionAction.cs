using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using iPet.Models;
using iPet.Models.Proc;
using System.Data.SqlClient;
using System.Data.Entity.Core.Objects;

namespace iPet.Utils
{
    public class RedirectingActionAttribute : ActionFilterAttribute
    {
        protected SiteContext db = new SiteContext();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            // Pega o objeto na sessão e converte para a o objeto tipo Usuario
            User user = (User)System.Web.HttpContext.Current.Session["User"];

            // Action do momento
            string actionName = filterContext.ActionDescriptor.ActionName;
            //Controller do momento
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            if (actionName == "Index") { actionName = ""; }

            if (user == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Conta",
                    action = "Entrar"
                }));
                return;
            }

            //Get User permission
            SqlParameter[] parameters;
            List<SP_UserPermissions> UserPermissions;

            // Procedure para pegar a lista de permissões
            string query = "SP_GETUSERPERMISSIONS @Id";

            parameters = new SqlParameter[]
            {
                new SqlParameter { ParameterName = "@Id",  Value = user.ID}
            };

            // Lista de Permissões do usuário
            UserPermissions = db.Database.SqlQuery<SP_UserPermissions>(query, parameters).ToList();

            // Lista de ID's das permissões
            List<int> PermissionsIDS = new List<int>();

            // Preenche a lista
            foreach (var item in UserPermissions)
            {
                PermissionsIDS.Add(item.ID);
            }

            if (controllerName != "Ajax" && (controllerName != "Conta" && actionName != "TrocarSenha"))
            {
                MenuItem _menu_item = db.MenuItems.First(mi => mi.Controller == controllerName && mi.Action == actionName);

                // Verifica se a ID contém na lista
                if (PermissionsIDS.IndexOf(_menu_item.ID) == -1)
                {
                    // Se a action for para um ajax retorna um json
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        filterContext.Result = new JsonResult
                        {
                            Data = new
                            {
                                success = false,
                                message = "Ops! Seems like you don't have any permission to do this."
                            },
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                    }
                    else
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        {
                            controller = "Erros",
                            action = "Error403"
                        }));
                        return;
                    }
                }
            }
        }
    }
}