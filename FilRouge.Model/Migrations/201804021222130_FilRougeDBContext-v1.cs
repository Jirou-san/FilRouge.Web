namespace FilRouge.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FilRougeDBContextv1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 20),
                        FirstName = c.String(nullable: false, maxLength: 20),
                        Phone = c.String(maxLength: 10),
                        Mail = c.String(nullable: false, maxLength: 30),
                        Type = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Quizzs",
                c => new
                    {
                        QuizzId = c.Int(nullable: false, identity: true),
                        QuizzState = c.Int(nullable: false),
                        UserLastName = c.String(nullable: false, maxLength: 20),
                        UserFirstName = c.String(nullable: false, maxLength: 20),
                        HasFreeQuestion = c.Boolean(nullable: false),
                        QuestionCount = c.Int(nullable: false),
                        DifficultyMasterId = c.Int(nullable: false),
                        TechnologyId = c.Int(nullable: false),
                        ContactId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuizzId)
                .ForeignKey("dbo.Contacts", t => t.ContactId)
                .ForeignKey("dbo.DifficultyMasters", t => t.DifficultyMasterId)
                .ForeignKey("dbo.Technologies", t => t.TechnologyId)
                .Index(t => t.DifficultyMasterId)
                .Index(t => t.TechnologyId)
                .Index(t => t.ContactId);
            
            CreateTable(
                "dbo.DifficultyMasters",
                c => new
                    {
                        DiffMasterId = c.Int(nullable: false, identity: true),
                        DiffMasterName = c.String(nullable: false, maxLength: 15),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DifficultyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DiffMasterId)
                .ForeignKey("dbo.DifficultyMasters", t => t.DifficultyId)
                .Index(t => t.DifficultyId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        QuestionId = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false, maxLength: 200),
                        Comment = c.String(maxLength: 500),
                        IsActive = c.Boolean(nullable: false),
                        QuestionTypeId = c.Int(nullable: false),
                        TechnologyId = c.Int(nullable: false),
                        DifficultyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuestionId)
                .ForeignKey("dbo.DifficultyMasters", t => t.DifficultyId)
                .ForeignKey("dbo.Technologies", t => t.TechnologyId)
                .ForeignKey("dbo.TypeQuestions", t => t.QuestionTypeId)
                .Index(t => t.QuestionTypeId)
                .Index(t => t.TechnologyId)
                .Index(t => t.DifficultyId);
            
            CreateTable(
                "dbo.Responses",
                c => new
                    {
                        ResponseId = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false, maxLength: 100),
                        IsTrue = c.Boolean(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ResponseId)
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Technologies",
                c => new
                    {
                        TechnoId = c.Int(nullable: false, identity: true),
                        TechnoName = c.String(nullable: false, maxLength: 20),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TechnoId);
            
            CreateTable(
                "dbo.TypeQuestions",
                c => new
                    {
                        TypeQuestionId = c.Int(nullable: false, identity: true),
                        NameType = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.TypeQuestionId);
            
            CreateTable(
                "dbo.UserResponses",
                c => new
                    {
                        QuizzId = c.Int(nullable: false),
                        ResponseId = c.Int(nullable: false),
                        Valeur = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => new { t.QuizzId, t.ResponseId })
                .ForeignKey("dbo.Quizzs", t => t.QuizzId)
                .ForeignKey("dbo.Responses", t => t.ResponseId)
                .Index(t => t.QuizzId)
                .Index(t => t.ResponseId);
            
            CreateTable(
                "dbo.QuestionQuizzs",
                c => new
                    {
                        Question_QuestionId = c.Int(nullable: false),
                        Quizz_QuizzId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Question_QuestionId, t.Quizz_QuizzId })
                .ForeignKey("dbo.Questions", t => t.Question_QuestionId, cascadeDelete: true)
                .ForeignKey("dbo.Quizzs", t => t.Quizz_QuizzId, cascadeDelete: true)
                .Index(t => t.Question_QuestionId)
                .Index(t => t.Quizz_QuizzId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserResponses", "ResponseId", "dbo.Responses");
            DropForeignKey("dbo.UserResponses", "QuizzId", "dbo.Quizzs");
            DropForeignKey("dbo.Quizzs", "TechnologyId", "dbo.Technologies");
            DropForeignKey("dbo.Quizzs", "DifficultyMasterId", "dbo.DifficultyMasters");
            DropForeignKey("dbo.DifficultyMasters", "DifficultyId", "dbo.DifficultyMasters");
            DropForeignKey("dbo.Questions", "QuestionTypeId", "dbo.TypeQuestions");
            DropForeignKey("dbo.Questions", "TechnologyId", "dbo.Technologies");
            DropForeignKey("dbo.Responses", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.QuestionQuizzs", "Quizz_QuizzId", "dbo.Quizzs");
            DropForeignKey("dbo.QuestionQuizzs", "Question_QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Questions", "DifficultyId", "dbo.DifficultyMasters");
            DropForeignKey("dbo.Quizzs", "ContactId", "dbo.Contacts");
            DropIndex("dbo.QuestionQuizzs", new[] { "Quizz_QuizzId" });
            DropIndex("dbo.QuestionQuizzs", new[] { "Question_QuestionId" });
            DropIndex("dbo.UserResponses", new[] { "ResponseId" });
            DropIndex("dbo.UserResponses", new[] { "QuizzId" });
            DropIndex("dbo.Responses", new[] { "QuestionId" });
            DropIndex("dbo.Questions", new[] { "DifficultyId" });
            DropIndex("dbo.Questions", new[] { "TechnologyId" });
            DropIndex("dbo.Questions", new[] { "QuestionTypeId" });
            DropIndex("dbo.DifficultyMasters", new[] { "DifficultyId" });
            DropIndex("dbo.Quizzs", new[] { "ContactId" });
            DropIndex("dbo.Quizzs", new[] { "TechnologyId" });
            DropIndex("dbo.Quizzs", new[] { "DifficultyMasterId" });
            DropTable("dbo.QuestionQuizzs");
            DropTable("dbo.UserResponses");
            DropTable("dbo.TypeQuestions");
            DropTable("dbo.Technologies");
            DropTable("dbo.Responses");
            DropTable("dbo.Questions");
            DropTable("dbo.DifficultyMasters");
            DropTable("dbo.Quizzs");
            DropTable("dbo.Contacts");
        }
    }
}
