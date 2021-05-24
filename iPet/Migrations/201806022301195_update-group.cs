namespace iPet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updategroup : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Groups", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Groups", "Name", c => c.String(nullable: false));
        }
    }
}
