namespace FilRouge.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FilRougeDBContext_v3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DifficultyRates", "Difficulty_DifficultyId", "dbo.Difficulties");
            DropIndex("dbo.DifficultyRates", new[] { "Difficulty_DifficultyId" });
            DropColumn("dbo.DifficultyRates", "Difficulty_DifficultyId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DifficultyRates", "Difficulty_DifficultyId", c => c.Int());
            CreateIndex("dbo.DifficultyRates", "Difficulty_DifficultyId");
            AddForeignKey("dbo.DifficultyRates", "Difficulty_DifficultyId", "dbo.Difficulties", "DifficultyId");
        }
    }
}
