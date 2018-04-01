namespace HatchlingCompany.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class workingonrelationship : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Relationships", name: "AuthorId", newName: "FirstEmployeeId");
            RenameColumn(table: "dbo.Relationships", name: "RecipientId", newName: "SecondEmployeeId");
            RenameIndex(table: "dbo.Relationships", name: "IX_AuthorId", newName: "IX_FirstEmployeeId");
            RenameIndex(table: "dbo.Relationships", name: "IX_RecipientId", newName: "IX_SecondEmployeeId");
            DropPrimaryKey("dbo.Relationships");
            AddPrimaryKey("dbo.Relationships", new[] { "FirstEmployeeId", "SecondEmployeeId" });
            DropColumn("dbo.Relationships", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Relationships", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Relationships");
            AddPrimaryKey("dbo.Relationships", "Id");
            RenameIndex(table: "dbo.Relationships", name: "IX_SecondEmployeeId", newName: "IX_RecipientId");
            RenameIndex(table: "dbo.Relationships", name: "IX_FirstEmployeeId", newName: "IX_AuthorId");
            RenameColumn(table: "dbo.Relationships", name: "SecondEmployeeId", newName: "RecipientId");
            RenameColumn(table: "dbo.Relationships", name: "FirstEmployeeId", newName: "AuthorId");
        }
    }
}
