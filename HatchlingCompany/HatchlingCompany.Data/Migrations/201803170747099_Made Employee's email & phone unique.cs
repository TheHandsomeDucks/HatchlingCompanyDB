namespace HatchlingCompany.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeEmployeesemailphoneunique : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "Email", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Employees", "Birthdate", c => c.DateTime());
            AlterColumn("dbo.Employees", "HireDate", c => c.DateTime());
            AlterColumn("dbo.Employees", "Status", c => c.Int());
            AlterColumn("dbo.Employees", "Salary", c => c.Decimal(precision: 18, scale: 2));
            CreateIndex("dbo.Employees", "Email", unique: true);
            CreateIndex("dbo.Employees", "PhoneNumber", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Employees", new[] { "PhoneNumber" });
            DropIndex("dbo.Employees", new[] { "Email" });
            AlterColumn("dbo.Employees", "Salary", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Employees", "Status", c => c.Int(nullable: false));
            AlterColumn("dbo.Employees", "HireDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Employees", "Birthdate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Employees", "Email", c => c.String(maxLength: 50));
        }
    }
}
