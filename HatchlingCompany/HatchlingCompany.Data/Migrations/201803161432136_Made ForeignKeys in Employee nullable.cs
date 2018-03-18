namespace HatchlingCompany.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeForeignKeysinEmployeenullable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Employees", new[] { "DepartmentId" });
            AlterColumn("dbo.Employees", "DepartmentId", c => c.Int());
            CreateIndex("dbo.Employees", "DepartmentId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Employees", new[] { "DepartmentId" });
            AlterColumn("dbo.Employees", "DepartmentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Employees", "DepartmentId");
        }
    }
}
