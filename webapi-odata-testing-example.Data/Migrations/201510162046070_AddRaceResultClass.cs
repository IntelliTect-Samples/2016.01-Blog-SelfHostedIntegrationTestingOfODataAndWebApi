namespace webapi_odata_testing_example.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRaceResultClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RaceResults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FinishTimeSpan = c.Time(precision: 7),
                        DidFinish = c.Boolean(nullable: false),
                        DidStart = c.Boolean(nullable: false),
                        LapsCompleted = c.Int(),
                        PitStops = c.Int(),
                        Car_Id = c.Int(),
                        Driver_Id = c.Int(),
                        Race_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.Car_Id)
                .ForeignKey("dbo.Drivers", t => t.Driver_Id)
                .ForeignKey("dbo.Races", t => t.Race_Id)
                .Index(t => t.Car_Id)
                .Index(t => t.Driver_Id)
                .Index(t => t.Race_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RaceResults", "Race_Id", "dbo.Races");
            DropForeignKey("dbo.RaceResults", "Driver_Id", "dbo.Drivers");
            DropForeignKey("dbo.RaceResults", "Car_Id", "dbo.Cars");
            DropIndex("dbo.RaceResults", new[] { "Race_Id" });
            DropIndex("dbo.RaceResults", new[] { "Driver_Id" });
            DropIndex("dbo.RaceResults", new[] { "Car_Id" });
            DropTable("dbo.RaceResults");
        }
    }
}
