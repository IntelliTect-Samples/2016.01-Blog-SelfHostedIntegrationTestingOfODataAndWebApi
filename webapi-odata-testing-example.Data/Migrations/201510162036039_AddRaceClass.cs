namespace webapi_odata_testing_example.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRaceClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Races",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Laps = c.Int(nullable: false),
                        StartDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Races");
        }
    }
}
