using iPet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iPet.Models
{
    public class Menu
    {
        public int ID { get; set; }

        public int? PaiID { get; set; }

        public string Titulo { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public string Icon { get; set; }

        public bool Multilevel { get; set; }

        public string Function { get; set; } 

        public List<MenuItem> Functions { get; set; }

        public List<Menu> SubMenu { get; set; }

        public string Print()
        {
            string _print = "";

            if (!Multilevel)
            {
                _print += string.Format(
                    "<li class='px-nav-item'> " +
                        "<a href='/{2}/{3}' style='text-decoration: none;'><i class='{0}'></i><span class='px-nav-label'>&nbsp{1}</span></a>" +
                    "</li>",
                    PaiID == 0 ? "px-nav-icon " + Icon : null,
                    Titulo,
                    Controller,
                    Action
                );
            }
            else
            {
                _print += string.Format(
                    "<li class='px-nav-item px-nav-dropdown'>" +
                        "<a href='#' style='text-decoration: none;'><i class='px-nav-icon {0}'></i><span class='px-nav-label'>&nbsp{1}</span></a>" +
                        "<ul class='px-nav-dropdown-menu'>", 
                    Icon, 
                    Titulo
                );

                foreach (var _sub in SubMenu) {
                    _print += _sub.Print();
                }

                _print += "</ul></li>";
            }

            return _print;
        }
    }
}