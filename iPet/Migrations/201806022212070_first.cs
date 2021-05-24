namespace iPet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GroupItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MenuItemID = c.Int(nullable: false),
                        GroupID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Groups", t => t.GroupID, cascadeDelete: true)
                .ForeignKey("dbo.MenuItems", t => t.MenuItemID, cascadeDelete: true)
                .Index(t => t.MenuItemID)
                .Index(t => t.GroupID);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MenuItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PaiID = c.Int(),
                        Index = c.String(),
                        Title = c.String(nullable: false),
                        Controller = c.String(),
                        Action = c.String(),
                        Icon = c.String(),
                        FlUnique = c.Boolean(nullable: false),
                        Function = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Pets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 20),
                        PetImage = c.String(),
                        Porte = c.Int(nullable: false),
                        Raca = c.String(nullable: false),
                        Castrado = c.Boolean(nullable: false),
                        Preco = c.Single(nullable: false),
                        Cor = c.Int(nullable: false),
                        Sexo = c.Int(nullable: false),
                        Vacinado = c.Boolean(nullable: false),
                        Description = c.String(),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Usuarios", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 20),
                        Avatar = c.String(),
                        Email = c.String(nullable: false),
                        Login = c.String(nullable: false),
                        Endereco = c.String(nullable: false),
                        Telefone = c.String(),
                        TipoUsuario = c.Int(nullable: false),
                        Cpf_Cnpj = c.String(nullable: false),
                        Senha = c.String(nullable: false),
                        GroupID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Groups", t => t.GroupID, cascadeDelete: true)
                .Index(t => t.GroupID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pets", "UserID", "dbo.Usuarios");
            DropForeignKey("dbo.Usuarios", "GroupID", "dbo.Groups");
            DropForeignKey("dbo.GroupItems", "MenuItemID", "dbo.MenuItems");
            DropForeignKey("dbo.GroupItems", "GroupID", "dbo.Groups");
            DropIndex("dbo.Usuarios", new[] { "GroupID" });
            DropIndex("dbo.Pets", new[] { "UserID" });
            DropIndex("dbo.GroupItems", new[] { "GroupID" });
            DropIndex("dbo.GroupItems", new[] { "MenuItemID" });
            DropTable("dbo.Usuarios");
            DropTable("dbo.Pets");
            DropTable("dbo.MenuItems");
            DropTable("dbo.Groups");
            DropTable("dbo.GroupItems");
        }
    }
}
