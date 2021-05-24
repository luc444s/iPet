namespace iPet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfavorites : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FavoritePets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        PetID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Pets", t => t.PetID, cascadeDelete: false)
                .ForeignKey("dbo.Usuarios", t => t.UserID, cascadeDelete: false)
                .Index(t => t.UserID)
                .Index(t => t.PetID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FavoritePets", "UserID", "dbo.Usuarios");
            DropForeignKey("dbo.FavoritePets", "PetID", "dbo.Pets");
            DropIndex("dbo.FavoritePets", new[] { "PetID" });
            DropIndex("dbo.FavoritePets", new[] { "UserID" });
            DropTable("dbo.FavoritePets");
        }
    }
}
