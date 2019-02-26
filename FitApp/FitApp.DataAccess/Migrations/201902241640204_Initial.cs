namespace FitApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointment",
                c => new
                    {
                        AppointmentId = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ScheduleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AppointmentId)
                .ForeignKey("dbo.GymClassSchedule", t => t.ScheduleId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ScheduleId);
            
            CreateTable(
                "dbo.GymClassSchedule",
                c => new
                    {
                        ScheduleId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        GymClassId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ScheduleId)
                .ForeignKey("dbo.GymClass", t => t.GymClassId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.GymClassId);
            
            CreateTable(
                "dbo.GymClass",
                c => new
                    {
                        GymClassId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        MaxClients = c.Int(),
                    })
                .PrimaryKey(t => t.GymClassId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointment", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Appointment", "ScheduleId", "dbo.GymClassSchedule");
            DropForeignKey("dbo.GymClassSchedule", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.GymClassSchedule", "GymClassId", "dbo.GymClass");
            DropIndex("dbo.GymClassSchedule", new[] { "GymClassId" });
            DropIndex("dbo.GymClassSchedule", new[] { "UserId" });
            DropIndex("dbo.Appointment", new[] { "ScheduleId" });
            DropIndex("dbo.Appointment", new[] { "UserId" });
            DropTable("dbo.GymClass");
            DropTable("dbo.GymClassSchedule");
            DropTable("dbo.Appointment");
        }
    }
}
