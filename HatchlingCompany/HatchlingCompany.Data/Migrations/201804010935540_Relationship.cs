namespace HatchlingCompany.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Relationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Relationships", "Employee_Id", "dbo.Employees");
            DropIndex("dbo.Relationships", new[] { "Employee_Id" });
            DropColumn("dbo.Relationships", "Employee_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Relationships", "Employee_Id", c => c.Int());
            CreateIndex("dbo.Relationships", "Employee_Id");
            AddForeignKey("dbo.Relationships", "Employee_Id", "dbo.Employees", "Id");
        }
    }
}
