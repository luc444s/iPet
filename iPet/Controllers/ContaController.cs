using iPet.Models;
using iPet.Models.Proc;
using iPet.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace iPet.Controllers
{
    public class ContaController : Controller
    {
        private SiteContext db = new SiteContext();

        [AllowAnonymous]
        public ActionResult Entrar()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Entrar(Login login)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                else
                {
                    string userName = login.Email;
                    string userPass = login.Password;

                    foreach (var user in db.Users.ToList())
                    {
                        if (user.Email == userName || user.Login == userName)
                        {
                            if (userPass == PassGenerator.Decrypt(user.Senha))
                            {
                                Session["UserID"] = user.ID;
                                Session["Name"] = user.Nome;
                                Session["UserType"] = user.TipoUsuario;
                                Session["UserName"] = userName;
                                Session["Avatar"] = user.Avatar;
                                Session["User"] = user;

                                db.SaveChanges();

                                if (user.TipoUsuario == TiposUsuarios.PessoaFisica)
                                {
                                    return RedirectToAction("Index", "Public");
                                }
                                else
                                {
                                    return RedirectToAction("Index", "Home");
                                }
                            }
                            else
                            {
                                throw new Exception("Senha inválida.");
                            }
                        }
                    }

                    throw new Exception("Usuário não encontrado.");
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                Session.Clear();
                return View();
            }
        }


        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User usuario)
        {
            try
            {
                //Just verify if there is any user with the same nickName
                foreach (var user in db.Users.ToList())
                {
                    if (user.Login == usuario.Login)
                    {
                        throw new Exception("Não foi possível realizar o cadastro, nome de usuário já utilizado!");
                    }

                    if (user.Email == usuario.Email)
                    {
                        throw new Exception("Não foi possível realizar o cadastro, email já utilizado por outro usuário!");
                    }

                    if (user.Cpf_Cnpj == usuario.Cpf_Cnpj)
                    {
                        if (usuario.TipoUsuario == TiposUsuarios.PessoaFisica)
                        {
                            throw new Exception("Não foi possível realizar o cadastro, cpf já utilizado por outra pessoa!");
                        }
                        else
                        {
                            throw new Exception("Não foi possível realizar o cadastro, cnpj já utilizado por outro usuário!");
                        }
                    }
                }

                usuario.Senha = PassGenerator.Encrypt(usuario.Senha);

                if (usuario.AvatarFile == null)
                {
                    usuario.Avatar = "default-avatar.png";
                }
                else
                {
                    string fileName = usuario.AvatarFile.FileName;

                    string path = Path.Combine(Server.MapPath("~/img/Avatars/"),
                                           Path.GetFileName(fileName));

                    usuario.AvatarFile.SaveAs(path);
                    usuario.Avatar = fileName;
                }

                if (ModelState.IsValid)
                {
                    if (usuario.TipoUsuario == TiposUsuarios.PessoaFisica)
                    {
                        usuario.GroupID = 1;
                    }
                    else
                    {
                        usuario.GroupID = 2;
                    }

                    db.Users.Add(usuario);
                    db.SaveChanges();

                    return RedirectToAction("Entrar");
                }
                else
                {

                    string errors = "";
                    foreach (ModelState modelState in ViewData.ModelState.Values)
                    {
                        foreach (ModelError error in modelState.Errors)
                        {
                            errors += error;
                        }
                    }

                    System.Diagnostics.Debug.WriteLine($"-----------{errors}");
                    return View();
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
        }

        [RedirectingActionAttribute]
        public ActionResult TrocarSenha()
        {
            try
            {
                ViewBag.Icon = "fa fa-cog";
                User user = db.Users.Find(Convert.ToInt32(Session["UserID"]));

                if (user == null)
                {
                    throw new Exception("HttpBadRequest");
                }

                return View();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return RedirectToAction("Error404", "Erros");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RedirectingActionAttribute]
        public ActionResult TrocarSenha(TrocarSenha _changePassword)
        {
            try
            {
                User usuario = db.Users.Find(_changePassword.Id);

                if (ModelState.IsValid)
                {
                    if (PassGenerator.Decrypt(usuario.Senha) != _changePassword.SenhaAntiga)
                    {
                        throw new Exception("Senha antiga inválida!");
                    }
                    else
                    {
                        if (_changePassword.NovaSenha == PassGenerator.Decrypt(usuario.Senha))
                        {
                            throw new Exception("A nova senha tem que ser diferente da antiga.");
                        }

                        usuario.Senha = PassGenerator.Encrypt(_changePassword.NovaSenha);
                        db.SaveChanges();
                        return RedirectToAction("Index", "Home", new { id = usuario.ID });
                    }
                }
                else
                {
                    return View();
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write("------------Erro: " + e);
                ViewBag.Error = e.Message;
                return View();
            }
        }

        public ActionResult Sair()
        {
            Session.Clear();
            return RedirectToAction("Entrar", "Conta");
        }
    }
}