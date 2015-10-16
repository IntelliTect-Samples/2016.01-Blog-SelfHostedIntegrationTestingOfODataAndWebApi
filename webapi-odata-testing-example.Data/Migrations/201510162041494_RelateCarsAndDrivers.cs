using System.Data.Entity.Migrations;

namespace Example.Data.Migrations
{
    public partial class RelateCarsAndDrivers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "Driver_Id", c => c.Int());
            CreateIndex("dbo.Cars", "Driver_Id");
            AddForeignKey("dbo.Cars", "Driver_Id", "dbo.Drivers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cars", "Driver_Id", "dbo.Drivers");
            DropIndex("dbo.Cars", new[] { "Driver_Id" });
            DropColumn("dbo.Cars", "Driver_Id");
        }
    }
}
