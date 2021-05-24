using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using iPet.Models;

namespace iPet.Models.Proc
{
    public class SP_UserPermissions
    {
        [Key]
        public int ID { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }
    }
}