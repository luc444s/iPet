using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace iPet.Models
{
    [Table("Groups")]
    public class Group
    {
        public Group()
        {
            
        }

        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public IEnumerator<User> Users { get; set; }

        public IEnumerator<GroupItem> GroupItems { get; set; }
    }
}