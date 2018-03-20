namespace HatchlingCompany.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class managerforprojectsettonullable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Projects", new[] { "ManagerId" });
            AlterColumn("dbo.Projects", "ManagerId", c => c.Int());
            CreateIndex("dbo.Projects", "ManagerId");
        }

        public override void Down()
        {
            DropIndex("dbo.Projects", new[] { "ManagerId" });
            AlterColumn("dbo.Projects", "ManagerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Projects", "ManagerId");
        }
    }
}
