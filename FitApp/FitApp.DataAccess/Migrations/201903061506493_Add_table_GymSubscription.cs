namespace FitApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_table_GymSubscription : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GymSubscriptions",
                c => new
                    {
                        SubscriptionId = c.Int(nullable: false, identity: true),
                        GymName = c.String(),
                        ValidFrom = c.DateTime(nullable: false),
                        ValidTo = c.DateTime(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SubscriptionId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GymSubscriptions", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.GymSubscriptions", new[] { "UserId" });
            DropTable("dbo.GymSubscriptions");
        }
    }
}
