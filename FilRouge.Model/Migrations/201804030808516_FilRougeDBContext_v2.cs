namespace FilRouge.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FilRougeDBContext_v2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Quizzs", "DifficultyRateId", "dbo.DifficultyRates");
            DropIndex("dbo.Quizzs", new[] { "DifficultyRateId" });
            AddColumn("dbo.Quizzs", "DifficultyId", c => c.Int(nullable: false));
            CreateIndex("dbo.Quizzs", "DifficultyId");
            AddForeignKey("dbo.Quizzs", "DifficultyId", "dbo.Difficulties", "DifficultyId");
            DropColumn("dbo.Quizzs", "DifficultyRateId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Quizzs", "DifficultyRateId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Quizzs", "DifficultyId", "dbo.Difficulties");
            DropIndex("dbo.Quizzs", new[] { "DifficultyId" });
            DropColumn("dbo.Quizzs", "DifficultyId");
            CreateIndex("dbo.Quizzs", "DifficultyRateId");
            AddForeignKey("dbo.Quizzs", "DifficultyRateId", "dbo.DifficultyRates", "DifficultyRateId");
        }
    }
}
