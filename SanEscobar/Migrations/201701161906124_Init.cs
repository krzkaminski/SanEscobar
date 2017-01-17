namespace SanEscobar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Attractions", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Attractions", "Image");
        }
    }
}
