namespace SanEscobar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Attractions", "Added", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Attractions", "Added", c => c.DateTime(nullable: false));
        }
    }
}
