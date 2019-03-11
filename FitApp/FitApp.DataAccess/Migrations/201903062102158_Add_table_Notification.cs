namespace FitApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_table_Notification : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        NotificationId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Message = c.String(),
                        Seen = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.NotificationId);
            
            CreateTable(
                "dbo.UserLoginHistories",
                c => new
                    {
                        UserLoginHistoryId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        LastLoggenOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserLoginHistoryId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserLoginHistories");
            DropTable("dbo.Notifications");
        }
    }
}
