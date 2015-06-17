namespace CheckInOut.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Branch",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Address = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        RouteData = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ConfigurationEmail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        SmtpClient = c.String(),
                        Port = c.Int(nullable: false),
                        EnableSsl = c.Boolean(nullable: false),
                        CcEmails = c.String(),
                        CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Device",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        BranchId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branch", t => t.BranchId, cascadeDelete: true)
                .Index(t => t.BranchId);
            
            CreateTable(
                "dbo.DeviceVisitorType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DeviceId = c.Int(nullable: false),
                        VisitorTypeId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Display = c.String(),
                        OrderNo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Device", t => t.DeviceId, cascadeDelete: true)
                .ForeignKey("dbo.VisitorType", t => t.VisitorTypeId, cascadeDelete: true)
                .Index(t => t.DeviceId)
                .Index(t => t.VisitorTypeId);
            
            CreateTable(
                "dbo.DeviceVisitorTypeDataField",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DeviceVisitorTypeId = c.Int(nullable: false),
                        DataFieldId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsRequired = c.Boolean(nullable: false),
                        OrderNo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DataField", t => t.DataFieldId, cascadeDelete: true)
                .ForeignKey("dbo.DeviceVisitorType", t => t.DeviceVisitorTypeId, cascadeDelete: true)
                .Index(t => t.DeviceVisitorTypeId)
                .Index(t => t.DataFieldId);
            
            CreateTable(
                "dbo.DataField",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        HtmlInputType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VisitorType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Visitor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Company = c.String(),
                        Email = c.String(),
                        Mobile = c.String(),
                        AccessNumber = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        VisitorTypeId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("dbo.VisitorType", t => t.VisitorTypeId, cascadeDelete: true)
                .Index(t => t.VisitorTypeId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.TrxVisit",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SignedIn = c.Boolean(nullable: false),
                        SignedOut = c.Boolean(),
                        TimeIn = c.DateTime(nullable: false),
                        TimeOut = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        DeviceInId = c.Int(nullable: false),
                        DeviceOutId = c.Int(),
                        VisitorId = c.Int(),
                        EmployeeId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employee", t => t.EmployeeId)
                .ForeignKey("dbo.Visitor", t => t.VisitorId)
                .Index(t => t.VisitorId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Mobile = c.String(),
                        AccessNumber = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        BranchId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branch", t => t.BranchId, cascadeDelete: true)
                .Index(t => t.BranchId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Visitor", "VisitorTypeId", "dbo.VisitorType");
            DropForeignKey("dbo.Visitor", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.TrxVisit", "VisitorId", "dbo.Visitor");
            DropForeignKey("dbo.TrxVisit", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.Employee", "BranchId", "dbo.Branch");
            DropForeignKey("dbo.DeviceVisitorType", "VisitorTypeId", "dbo.VisitorType");
            DropForeignKey("dbo.DeviceVisitorTypeDataField", "DeviceVisitorTypeId", "dbo.DeviceVisitorType");
            DropForeignKey("dbo.DeviceVisitorTypeDataField", "DataFieldId", "dbo.DataField");
            DropForeignKey("dbo.DeviceVisitorType", "DeviceId", "dbo.Device");
            DropForeignKey("dbo.Device", "BranchId", "dbo.Branch");
            DropForeignKey("dbo.ConfigurationEmail", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Branch", "CompanyId", "dbo.Company");
            DropIndex("dbo.Employee", new[] { "BranchId" });
            DropIndex("dbo.TrxVisit", new[] { "EmployeeId" });
            DropIndex("dbo.TrxVisit", new[] { "VisitorId" });
            DropIndex("dbo.Visitor", new[] { "CompanyId" });
            DropIndex("dbo.Visitor", new[] { "VisitorTypeId" });
            DropIndex("dbo.DeviceVisitorTypeDataField", new[] { "DataFieldId" });
            DropIndex("dbo.DeviceVisitorTypeDataField", new[] { "DeviceVisitorTypeId" });
            DropIndex("dbo.DeviceVisitorType", new[] { "VisitorTypeId" });
            DropIndex("dbo.DeviceVisitorType", new[] { "DeviceId" });
            DropIndex("dbo.Device", new[] { "BranchId" });
            DropIndex("dbo.ConfigurationEmail", new[] { "CompanyId" });
            DropIndex("dbo.Branch", new[] { "CompanyId" });
            DropTable("dbo.Employee");
            DropTable("dbo.TrxVisit");
            DropTable("dbo.Visitor");
            DropTable("dbo.VisitorType");
            DropTable("dbo.DataField");
            DropTable("dbo.DeviceVisitorTypeDataField");
            DropTable("dbo.DeviceVisitorType");
            DropTable("dbo.Device");
            DropTable("dbo.ConfigurationEmail");
            DropTable("dbo.Company");
            DropTable("dbo.Branch");
        }
    }
}
