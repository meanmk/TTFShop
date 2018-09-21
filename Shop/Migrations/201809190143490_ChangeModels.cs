namespace Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "ProductId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetails", "ProductId");
        }
    }
}
