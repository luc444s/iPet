using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iPet.Models
{
    public class MenuItem
    {
        [Key]
        public int ID { get; set; }

        public int? PaiID { get; set; }

        public string Index { get; set; }

        [Required]
        public string Title { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public string Icon { get; set; }

        [Required]
        public bool FlUnique { get; set; }

        public string Function { get; set; }

        public IEnumerable<GroupItem> Items { get; set; }
    }
}