using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace iPet.Models
{
    public class FavoritePet
    {
        [Key]
        public int ID { get; set; }

        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        public int PetID { get; set; }

        [ForeignKey("PetID")]
        public virtual Pet Pet { get; set; }
    }
}