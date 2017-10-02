namespace TelerikMvcApp10.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingData : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "Category", c => c.String());
            AddColumn("dbo.Cars", "InStock", c => c.Int(nullable: false));
            AddColumn("dbo.Cars", "Discontinued", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cars", "Discontinued");
            DropColumn("dbo.Cars", "InStock");
            DropColumn("dbo.Cars", "Category");
        }
    }
}
