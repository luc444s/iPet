using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using iPet.Models;

namespace iPet.Models.Proc
{
    public class SP_MenuPermissions
    {
        [Key]
        public int ID { get; set; }

        public string Index { get; set; }

        public int? PaiID { get; set; }

        public string Titulo { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public string Icon { get; set; }

        public bool FlUnico { get; set; }

        public bool FlAtivo { get; set; }

        public bool Function { get; set; }

        public static List<Menu> CreateMenu(List<SP_MenuPermissions> permissoes)
        {
            List<Menu> menu = new List<Menu>();

            foreach (var _m in permissoes)
            {
                if (_m.Index != null) {
                    if (_m.FlAtivo && _m.PaiID == 0)
                    {
                        Menu m = new Menu
                        {
                            PaiID = _m.PaiID,
                            Titulo = _m.Titulo,
                            Controller = _m.Controller,
                            Action = _m.Action,
                            Icon = _m.Icon,
                            Multilevel = !_m.FlUnico,
                            SubMenu = new List<Menu>()
                        };

                        if (!_m.FlUnico)
                        {
                            List<SP_MenuPermissions> submenus = permissoes.FindAll(p => p.PaiID.Equals(_m.ID));

                            foreach (var _s in submenus)
                            {
                                if (_s.FlAtivo)
                                {
                                    Menu sub_m = new Menu
                                    {
                                        PaiID = _s.PaiID,
                                        Titulo = _s.Titulo,
                                        Controller = _s.Controller,
                                        Action = _s.Action,
                                        Icon = _s.Icon,
                                        Multilevel = false,
                                        SubMenu = null
                                    };

                                    m.SubMenu.Add(sub_m);
                                }
                            }
                        }

                        menu.Add(m);
                    }
                }                
            }

            return menu;
        }
    }
}