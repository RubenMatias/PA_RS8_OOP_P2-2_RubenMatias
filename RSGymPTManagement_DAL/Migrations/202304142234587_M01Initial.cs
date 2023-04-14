namespace RSGymPTManagement_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M01Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientID = c.Int(nullable: false, identity: true),
                        PostalCodeID = c.Int(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        BirthDate = c.DateTime(nullable: false),
                        NIF = c.String(nullable: false, maxLength: 9),
                        Adress = c.String(nullable: false, maxLength: 100),
                        PhoneNumber = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Observations = c.String(),
                        Status = c.Boolean(nullable: false),
                        StatusDescription = c.String(),
                        Users_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.ClientID)
                .ForeignKey("dbo.PostalCodes", t => t.PostalCodeID)
                .ForeignKey("dbo.Users", t => t.Users_UserID)
                .Index(t => t.PostalCodeID)
                .Index(t => t.Users_UserID);
            
            CreateTable(
                "dbo.PostalCodes",
                c => new
                    {
                        PostalCodeID = c.Int(nullable: false, identity: true),
                        PostalCodeValue = c.String(nullable: false),
                        Town = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.PostalCodeID);
            
            CreateTable(
                "dbo.PersonalTrainers",
                c => new
                    {
                        PersonalTrainerID = c.Int(nullable: false, identity: true),
                        PostalCodeID = c.Int(nullable: false),
                        Code = c.String(nullable: false, maxLength: 4),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        NIF = c.String(nullable: false, maxLength: 9),
                        Adress = c.String(nullable: false, maxLength: 100),
                        PhoneNumber = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Users_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.PersonalTrainerID)
                .ForeignKey("dbo.PostalCodes", t => t.PostalCodeID)
                .ForeignKey("dbo.Users", t => t.Users_UserID)
                .Index(t => t.PostalCodeID)
                .Index(t => t.Users_UserID);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        RequestId = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        PersonalTrainerId = c.Int(nullable: false),
                        Schedule = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        Observation = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.RequestId)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .ForeignKey("dbo.PersonalTrainers", t => t.PersonalTrainerId)
                .Index(t => t.ClientId)
                .Index(t => t.PersonalTrainerId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Code = c.String(nullable: false, maxLength: 6),
                        Password = c.String(nullable: false, maxLength: 12),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PersonalTrainers", "Users_UserID", "dbo.Users");
            DropForeignKey("dbo.Clients", "Users_UserID", "dbo.Users");
            DropForeignKey("dbo.Requests", "PersonalTrainerId", "dbo.PersonalTrainers");
            DropForeignKey("dbo.Requests", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.PersonalTrainers", "PostalCodeID", "dbo.PostalCodes");
            DropForeignKey("dbo.Clients", "PostalCodeID", "dbo.PostalCodes");
            DropIndex("dbo.Requests", new[] { "PersonalTrainerId" });
            DropIndex("dbo.Requests", new[] { "ClientId" });
            DropIndex("dbo.PersonalTrainers", new[] { "Users_UserID" });
            DropIndex("dbo.PersonalTrainers", new[] { "PostalCodeID" });
            DropIndex("dbo.Clients", new[] { "Users_UserID" });
            DropIndex("dbo.Clients", new[] { "PostalCodeID" });
            DropTable("dbo.Users");
            DropTable("dbo.Requests");
            DropTable("dbo.PersonalTrainers");
            DropTable("dbo.PostalCodes");
            DropTable("dbo.Clients");
        }
    }
}
