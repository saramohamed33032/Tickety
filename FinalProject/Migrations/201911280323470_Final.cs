namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Final : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Last_Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Last_Name", c => c.String(nullable: false));
        }
    }
}
