using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace iPet.Models
{
    [Table("GroupItems")]
    public class GroupItem
    {
        [Key]
        public int ID { get; set; }

        public int MenuItemID { get; set; }
        
        public int GroupID { get; set; }

        [ForeignKey("MenuItemID")]
        public MenuItem MenuItems { get; set; }

        [ForeignKey("GroupID")]
        public virtual Group Group { get; set; }

        public GroupItem() {
        }
    }
}