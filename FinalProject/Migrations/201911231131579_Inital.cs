namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inital : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Degree",
                c => new
                    {
                        ticketClass = c.String(nullable: false, maxLength: 1),
                        matchID = c.String(nullable: false, maxLength: 128),
                        degreePrice = c.Double(nullable: false),
                        NOSeat = c.Int(name: "NO.Seat", nullable: false),
                    })
                .PrimaryKey(t => new { t.ticketClass, t.matchID })
                .ForeignKey("dbo.Matches", t => t.matchID, cascadeDelete: true)
                .Index(t => t.matchID);
            
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        matchID = c.String(nullable: false, maxLength: 128),
                        matchDate = c.DateTime(nullable: false),
                        matchTime = c.Time(nullable: false, precision: 7),
                        isValid = c.Boolean(nullable: false),
                        staduimID = c.String(maxLength: 128),
                        HomeTeamId = c.String(maxLength: 128),
                        GuestTeamId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.matchID)
                .ForeignKey("dbo.Teams", t => t.GuestTeamId)
                .ForeignKey("dbo.Teams", t => t.HomeTeamId)
                .ForeignKey("dbo.Staduims", t => t.staduimID)
                .Index(t => t.staduimID)
                .Index(t => t.HomeTeamId)
                .Index(t => t.GuestTeamId);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        teamID = c.String(nullable: false, maxLength: 128),
                        teamName = c.String(nullable: false, maxLength: 50),
                        teamLogo = c.String(nullable: false),
                        isValid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.teamID);
            
            CreateTable(
                "dbo.Staduims",
                c => new
                    {
                        staduimID = c.String(nullable: false, maxLength: 128),
                        staduimName = c.String(nullable: false, maxLength: 50),
                        staduimLocation = c.String(nullable: false),
                        city = c.String(nullable: false),
                        isValid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.staduimID);
            
            CreateTable(
                "dbo.Ticket",
                c => new
                    {
                        ticketID = c.String(nullable: false, maxLength: 128),
                        ticketClass = c.String(maxLength: 128),
                        matchID = c.String(maxLength: 1),
                        UserID = c.String(maxLength: 128),
                        Seat_Number = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ticketID)
                .ForeignKey("dbo.Degree", t => new { t.matchID, t.ticketClass })
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => new { t.matchID, t.ticketClass })
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Frist_Name = c.String(nullable: false),
                        Last_Name = c.String(nullable: false),
                        gender = c.Int(nullable: false),
                        ProfileImage = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Ticket", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ticket", new[] { "matchID", "ticketClass" }, "dbo.Degree");
            DropForeignKey("dbo.Degree", "matchID", "dbo.Matches");
            DropForeignKey("dbo.Matches", "staduimID", "dbo.Staduims");
            DropForeignKey("dbo.Matches", "HomeTeamId", "dbo.Teams");
            DropForeignKey("dbo.Matches", "GuestTeamId", "dbo.Teams");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Ticket", new[] { "UserID" });
            DropIndex("dbo.Ticket", new[] { "matchID", "ticketClass" });
            DropIndex("dbo.Matches", new[] { "GuestTeamId" });
            DropIndex("dbo.Matches", new[] { "HomeTeamId" });
            DropIndex("dbo.Matches", new[] { "staduimID" });
            DropIndex("dbo.Degree", new[] { "matchID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Ticket");
            DropTable("dbo.Staduims");
            DropTable("dbo.Teams");
            DropTable("dbo.Matches");
            DropTable("dbo.Degree");
        }
    }
}
