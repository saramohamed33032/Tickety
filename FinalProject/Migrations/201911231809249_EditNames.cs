namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditNames : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Matches", newName: "Match");
            RenameTable(name: "dbo.Teams", newName: "Team");
            RenameTable(name: "dbo.Staduims", newName: "Staduim");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Staduim", newName: "Staduims");
            RenameTable(name: "dbo.Team", newName: "Teams");
            RenameTable(name: "dbo.Match", newName: "Matches");
        }
    }
}
