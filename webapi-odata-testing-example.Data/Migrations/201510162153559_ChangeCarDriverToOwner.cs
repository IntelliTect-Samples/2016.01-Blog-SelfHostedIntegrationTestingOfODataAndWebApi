namespace Example.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeCarDriverToOwner : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Cars", name: "Driver_Id", newName: "Owner_Id");
            RenameIndex(table: "dbo.Cars", name: "IX_Driver_Id", newName: "IX_Owner_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Cars", name: "IX_Owner_Id", newName: "IX_Driver_Id");
            RenameColumn(table: "dbo.Cars", name: "Owner_Id", newName: "Driver_Id");
        }
    }
}
