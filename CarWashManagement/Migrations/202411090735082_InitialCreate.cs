namespace CarWashManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Email = c.String(),
                        VehicleReg = c.String(),
                        Points = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClientID);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        VehicleID = c.Int(nullable: false, identity: true),
                        VehicleType = c.String(),
                    })
                .PrimaryKey(t => t.VehicleID);
            
            CreateTable(
                "dbo.VehicleWashes",
                c => new
                    {
                        RefNo = c.String(nullable: false, maxLength: 128),
                        ClientID = c.Int(nullable: false),
                        VehicleID = c.Int(nullable: false),
                        WashId = c.Int(nullable: false),
                        Make = c.String(),
                        VehicleReg = c.String(),
                        cost = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.RefNo)
                .ForeignKey("dbo.Clients", t => t.ClientID, cascadeDelete: true)
                .ForeignKey("dbo.Vehicles", t => t.VehicleID, cascadeDelete: true)
                .ForeignKey("dbo.Washes", t => t.WashId, cascadeDelete: true)
                .Index(t => t.ClientID)
                .Index(t => t.VehicleID)
                .Index(t => t.WashId);
            
            CreateTable(
                "dbo.Washes",
                c => new
                    {
                        WashId = c.Int(nullable: false, identity: true),
                        WashType = c.String(),
                        Cost = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.WashId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VehicleWashes", "WashId", "dbo.Washes");
            DropForeignKey("dbo.VehicleWashes", "VehicleID", "dbo.Vehicles");
            DropForeignKey("dbo.VehicleWashes", "ClientID", "dbo.Clients");
            DropIndex("dbo.VehicleWashes", new[] { "WashId" });
            DropIndex("dbo.VehicleWashes", new[] { "VehicleID" });
            DropIndex("dbo.VehicleWashes", new[] { "ClientID" });
            DropTable("dbo.Washes");
            DropTable("dbo.VehicleWashes");
            DropTable("dbo.Vehicles");
            DropTable("dbo.Clients");
        }
    }
}
