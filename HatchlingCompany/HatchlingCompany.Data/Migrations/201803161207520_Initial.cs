namespace HatchlingCompany.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Towns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        TownId = c.Int(nullable: false),
                        ManagerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.ManagerId)
                .ForeignKey("dbo.Towns", t => t.TownId)
                .Index(t => t.TownId)
                .Index(t => t.ManagerId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 50),
                        MiddleName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        Email = c.String(maxLength: 50),
                        PhoneNumber = c.String(maxLength: 30),
                        Birthdate = c.DateTime(nullable: false),
                        HireDate = c.DateTime(nullable: false),
                        BankAccount = c.String(maxLength: 30),
                        Status = c.Int(nullable: false),
                        Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        JobTitle = c.String(maxLength: 30),
                        EmployeeDetailsId = c.Int(nullable: false),
                        ManagerId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.ManagerId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .Index(t => t.ManagerId)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.EmployeeDetails",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Detail = c.String(),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Departments", "TownId", "dbo.Towns");
            DropForeignKey("dbo.Departments", "ManagerId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Employees", "ManagerId", "dbo.Employees");
            DropForeignKey("dbo.EmployeeDetails", "Id", "dbo.Employees");
            DropForeignKey("dbo.Towns", "CountryId", "dbo.Countries");
            DropIndex("dbo.EmployeeDetails", new[] { "Id" });
            DropIndex("dbo.Employees", new[] { "DepartmentId" });
            DropIndex("dbo.Employees", new[] { "ManagerId" });
            DropIndex("dbo.Departments", new[] { "ManagerId" });
            DropIndex("dbo.Departments", new[] { "TownId" });
            DropIndex("dbo.Towns", new[] { "CountryId" });
            DropTable("dbo.EmployeeDetails");
            DropTable("dbo.Employees");
            DropTable("dbo.Departments");
            DropTable("dbo.Towns");
            DropTable("dbo.Countries");
        }
    }
}
