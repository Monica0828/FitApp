using FitApp.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace FitApp.DataAccess
{
    public partial class FitAppModel : IdentityDbContext<FitAppUser>
    {
        public FitAppModel()
            : base("name=FitAppModel1")
        {
        }

        public virtual DbSet<GymClass> GymClasses { get; set; }
        public virtual DbSet<GymClassSchedule> GymClassSchedules { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<GymSubscription> GymSubscriptions { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<UserLoginHistory> UserHistory { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GymClassSchedule>()
                    .HasRequired(cs => cs.Trainer)
                    .WithMany()
                    .HasForeignKey(x => x.UserId)
                    .WillCascadeOnDelete(false);

            modelBuilder.Entity<GymClassSchedule>()
                    .HasRequired(cs => cs.Class)
                    .WithMany()
                    .HasForeignKey(x => x.GymClassId)
                    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Appointment>()
                    .HasRequired(cs => cs.Client)
                    .WithMany()
                    .HasForeignKey(x => x.UserId)
                    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Appointment>()
                    .HasRequired(cs => cs.ClassSchedule)
                    .WithMany()
                    .HasForeignKey(x => x.ScheduleId)
                    .WillCascadeOnDelete(false);

            modelBuilder.Entity<GymSubscription>()
                    .HasRequired(gs => gs.Client)
                    .WithMany()
                    .HasForeignKey(x => x.UserId)
                    .WillCascadeOnDelete(false);

        }
    }
}
