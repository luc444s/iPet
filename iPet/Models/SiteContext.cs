namespace iPet.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SiteContext : DbContext
    {
        public SiteContext()
            : base("name=SiteContext")
        {
        }

        public virtual DbSet<Pet> Pets { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Group> Groups { get; set; }        
        public virtual DbSet<GroupItem> GroupItems { get; set; }        
        public virtual DbSet<MenuItem> MenuItems { get; set; } 
        public virtual DbSet<FavoritePet> FavoritePet { get; set; }
        
    }
}
