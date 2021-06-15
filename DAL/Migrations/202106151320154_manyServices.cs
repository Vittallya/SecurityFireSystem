namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class manyServices : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "ServiceId", "dbo.Services");
            DropIndex("dbo.Orders", new[] { "ServiceId" });
            CreateTable(
                "dbo.ServiceOrders",
                c => new
                    {
                        Service_Id = c.Int(nullable: false),
                        Order_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Service_Id, t.Order_Id })
                .ForeignKey("dbo.Services", t => t.Service_Id, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.Order_Id, cascadeDelete: true)
                .Index(t => t.Service_Id)
                .Index(t => t.Order_Id);
            
            AddColumn("dbo.Clients", "INN", c => c.String());
            AddColumn("dbo.Clients", "CompanyName", c => c.String());
            DropColumn("dbo.Orders", "ServiceId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "ServiceId", c => c.Int(nullable: false));
            DropForeignKey("dbo.ServiceOrders", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.ServiceOrders", "Service_Id", "dbo.Services");
            DropIndex("dbo.ServiceOrders", new[] { "Order_Id" });
            DropIndex("dbo.ServiceOrders", new[] { "Service_Id" });
            DropColumn("dbo.Clients", "CompanyName");
            DropColumn("dbo.Clients", "INN");
            DropTable("dbo.ServiceOrders");
            CreateIndex("dbo.Orders", "ServiceId");
            AddForeignKey("dbo.Orders", "ServiceId", "dbo.Services", "Id", cascadeDelete: true);
        }
    }
}
