namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FF : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Frist_Name", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Frist_Name", c => c.String(nullable: false));
        }
    }
}
