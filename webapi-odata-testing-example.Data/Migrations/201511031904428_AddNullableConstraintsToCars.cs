namespace Example.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNullableConstraintsToCars : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cars", "Owner_Id", "dbo.Drivers");
            DropIndex("dbo.Cars", new[] { "Owner_Id" });
            AlterColumn("dbo.Cars", "Owner_Id", c => c.Int());
            CreateIndex("dbo.Cars", "Owner_Id");
            AddForeignKey("dbo.Cars", "Owner_Id", "dbo.Drivers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cars", "Owner_Id", "dbo.Drivers");
            DropIndex("dbo.Cars", new[] { "Owner_Id" });
            AlterColumn("dbo.Cars", "Owner_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Cars", "Owner_Id");
            AddForeignKey("dbo.Cars", "Owner_Id", "dbo.Drivers", "Id", cascadeDelete: true);
        }
    }
}
