namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editclass : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Ticket", new[] { "matchID", "ticketClass" });
            RenameColumn(table: "dbo.Ticket", name: "matchID", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.Ticket", name: "ticketClass", newName: "matchID");
            RenameColumn(table: "dbo.Ticket", name: "__mig_tmp__0", newName: "ticketClass");
            AlterColumn("dbo.Ticket", "ticketClass", c => c.String(maxLength: 1));
            AlterColumn("dbo.Ticket", "matchID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Ticket", new[] { "ticketClass", "matchID" });
        }
        
        public override void Down()
        {
            DropIndex("dbo.Ticket", new[] { "ticketClass", "matchID" });
            AlterColumn("dbo.Ticket", "matchID", c => c.String(maxLength: 1));
            AlterColumn("dbo.Ticket", "ticketClass", c => c.String(maxLength: 128));
            RenameColumn(table: "dbo.Ticket", name: "ticketClass", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.Ticket", name: "matchID", newName: "ticketClass");
            RenameColumn(table: "dbo.Ticket", name: "__mig_tmp__0", newName: "matchID");
            CreateIndex("dbo.Ticket", new[] { "matchID", "ticketClass" });
        }
    }
}
