namespace HatchlingCompany.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init183 : DbMigration
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
                        Email = c.String(nullable: false, maxLength: 50),
                        PhoneNumber = c.String(maxLength: 30),
                        Birthdate = c.DateTime(),
                        HireDate = c.DateTime(),
                        BankAccount = c.String(maxLength: 30),
                        Status = c.Int(),
                        Salary = c.Decimal(precision: 18, scale: 2),
                        JobTitle = c.String(maxLength: 30),
                        ManagerId = c.Int(),
                        DepartmentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.ManagerId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .Index(t => t.Email, unique: true)
                .Index(t => t.PhoneNumber, unique: true)
                .Index(t => t.ManagerId)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                        Detail = c.String(),
                        ManagerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.ManagerId)
                .Index(t => t.Name, unique: true)
                .Index(t => t.ManagerId);
            
            CreateTable(
                "dbo.Relationships",
                c => new
                    {
                        FirstEmployeeId = c.Int(nullable: false),
                        SecondEmployeeId = c.Int(nullable: false),
                        Comment = c.String(),
                        RelationshipStrength = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FirstEmployeeId, t.SecondEmployeeId })
                .ForeignKey("dbo.Employees", t => t.FirstEmployeeId)
                .ForeignKey("dbo.Employees", t => t.SecondEmployeeId)
                .Index(t => t.FirstEmployeeId)
                .Index(t => t.SecondEmployeeId);
            
            CreateTable(
                "dbo.EmployeeProjects",
                c => new
                    {
                        Employee_Id = c.Int(nullable: false),
                        Project_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Employee_Id, t.Project_Id })
                .ForeignKey("dbo.Employees", t => t.Employee_Id)
                .ForeignKey("dbo.Projects", t => t.Project_Id)
                .Index(t => t.Employee_Id)
                .Index(t => t.Project_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Relationships", "SecondEmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Relationships", "FirstEmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Departments", "TownId", "dbo.Towns");
            DropForeignKey("dbo.Departments", "ManagerId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.EmployeeProjects", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.EmployeeProjects", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.Projects", "ManagerId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "ManagerId", "dbo.Employees");
            DropForeignKey("dbo.Towns", "CountryId", "dbo.Countries");
            DropIndex("dbo.EmployeeProjects", new[] { "Project_Id" });
            DropIndex("dbo.EmployeeProjects", new[] { "Employee_Id" });
            DropIndex("dbo.Relationships", new[] { "SecondEmployeeId" });
            DropIndex("dbo.Relationships", new[] { "FirstEmployeeId" });
            DropIndex("dbo.Projects", new[] { "ManagerId" });
            DropIndex("dbo.Projects", new[] { "Name" });
            DropIndex("dbo.Employees", new[] { "DepartmentId" });
            DropIndex("dbo.Employees", new[] { "ManagerId" });
            DropIndex("dbo.Employees", new[] { "PhoneNumber" });
            DropIndex("dbo.Employees", new[] { "Email" });
            DropIndex("dbo.Departments", new[] { "ManagerId" });
            DropIndex("dbo.Departments", new[] { "TownId" });
            DropIndex("dbo.Towns", new[] { "CountryId" });
            DropTable("dbo.EmployeeProjects");
            DropTable("dbo.Relationships");
            DropTable("dbo.Projects");
            DropTable("dbo.Employees");
            DropTable("dbo.Departments");
            DropTable("dbo.Towns");
            DropTable("dbo.Countries");
        }
    }
}
