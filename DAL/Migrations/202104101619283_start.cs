namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class start : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Login = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        ServiceId = c.Int(nullable: false),
                        FullCost = c.Double(nullable: false),
                        CreationDate = c.DateTimeOffset(nullable: false, precision: 7),
                        PersonalSale = c.Double(nullable: false),
                        Address = c.String(),
                        OrderStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.ServiceId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Cost = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PersonalSales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        SaleValue = c.Double(nullable: false),
                        IsActual = c.Boolean(nullable: false),
                        Service_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.Service_Id)
                .Index(t => t.ClientId)
                .Index(t => t.Service_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PersonalSales", "Service_Id", "dbo.Services");
            DropForeignKey("dbo.PersonalSales", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Orders", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.Orders", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Profiles", "Id", "dbo.Clients");
            DropIndex("dbo.PersonalSales", new[] { "Service_Id" });
            DropIndex("dbo.PersonalSales", new[] { "ClientId" });
            DropIndex("dbo.Orders", new[] { "ServiceId" });
            DropIndex("dbo.Orders", new[] { "ClientId" });
            DropIndex("dbo.Profiles", new[] { "Id" });
            DropTable("dbo.PersonalSales");
            DropTable("dbo.Services");
            DropTable("dbo.Orders");
            DropTable("dbo.Profiles");
            DropTable("dbo.Clients");
        }
    }
}
