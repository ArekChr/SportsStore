namespace SportsStore.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Product : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "cena");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "cena", c => c.String());
        }
    }
}
