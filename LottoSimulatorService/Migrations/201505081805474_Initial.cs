namespace LottoSimulatorService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("LottoSimulatorService.Lottoes", "longitude", c => c.Double(nullable: false));
            AddColumn("LottoSimulatorService.Lottoes", "latitude", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("LottoSimulatorService.Lottoes", "latitude");
            DropColumn("LottoSimulatorService.Lottoes", "longitude");
        }
    }
}
