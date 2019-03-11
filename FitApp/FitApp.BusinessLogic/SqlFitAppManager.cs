using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FitApp.DataAccess;
using FitApp.Interfaces;
using FitApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FitApp.BusinessLogic
{
    public class SqlFitAppManager : IFitAppManager
    {
        public FitAppModel Context { get; set; }

        public SqlFitAppManager()
        {
            Context = new FitAppModel();
        }

        public GymClass Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IList<GymClass> GetAllClasses()
        {
            return Context.GymClasses.ToList();
        }

        public IList<GymSubscription> GetSubscriptions(string clientId)
        {
            return Context.GymSubscriptions
                            .Where(a => a.UserId == clientId)
                            .Include(a => a.Client)
                            .ToList();
        }
        public IList<Appointment> GetAllAppointment(string clientId)
        {
            return Context.Appointments
                .Where(a => a.UserId == clientId)
                .Include(a => a.ClassSchedule)
                .Include(a => a.Client)
                .ToList();
        }

        public IList<int> GetScheduleIdForClient(string clientId)
        {
            return Context.Appointments
                .Where(a => a.UserId == clientId)
                .Select(a => a.ScheduleId)
                .ToList();
        }

        public List<GymClass> GetNewGymClasses(DateTime loggedOn, string clientId)
        {
            var list = Context.Notifications.Where(n => n.UserId == clientId).Select(n => n.GymClassId).ToList();

            return Context.GymClasses
                .Where(gc => !list.Contains(gc.GymClassId))
                .Where(gc => gc.DateCreated > loggedOn)
                .ToList();
        }

        public List<Notification> GetNotifications(string clientId)
        {
            var list= Context.Notifications.Where(n => n.UserId == clientId).OrderByDescending(n => n.NotificationId).Take(10).ToList();
            return list;
        }

        public void MarkNotificationsAsRead(List<Notification> list)
        {
            foreach(var item in list)
            {
                item.Seen = true;
                Context.SaveChanges();
            }
        }

        public IList<GymClassSchedule> GetSchedule(string clientId)
        {
            var clientSchedules = this.GetScheduleIdForClient(clientId);

            return Context.GymClassSchedules
                .Where(s => !clientSchedules.Contains(s.ScheduleId))
                .Include(s => s.Class)
                .Include(s => s.Trainer)
                .OrderBy(s => s.Trainer.FirstName)
                .ThenBy(s => s.Date)
                .ToList();
        }

        public GymClassSchedule GetGymClassSchedule(int scheduleId)
        {
            return Context.GymClassSchedules.FirstOrDefault(g => g.ScheduleId == scheduleId);
        }

        public string GetTrainerForGymClassSchedule(int scheduleId)
        {
            var user = Context.GymClassSchedules.Where(gc => gc.ScheduleId == scheduleId).Select(gc => gc.UserId).Single();
            return user;
        }

        public object GetUserManager(Login loginCredentials)
        {
            var store = new UserStore<FitAppUser>(Context);
            var userManager = new UserManager<FitAppUser>(store);
            return userManager;
        }
        public IList<GymClassSchedule> GetTrainerSchedule(string trainerId)
        {
            return Context.GymClassSchedules.Where(gc => gc.UserId == trainerId).ToList();
        }
        public DateTime GetLastLoggenOn(string userId)
        {
            var loggedOn = Context.UserHistory.SingleOrDefault(u => u.UserId == userId);

            if (loggedOn != null)
                return loggedOn.LastLoggenOn;
            else
                return DateTime.MinValue;
        }

        

        public FitAppUser FindUser(Login loginCredentials)
        {
            var store = new UserStore<FitAppUser>(Context);
            var userManager = new UserManager<FitAppUser>(store);

            return userManager.Find(loginCredentials.Email, loginCredentials.Password);
        }

        public void RegisterUser(FitAppUser user, string password, string role)
        {
            var store = new UserStore<FitAppUser>(Context);
            var userManager = new UserManager<FitAppUser>(store);

            var result = userManager.Create(user, password);
            if (result.Succeeded)
            {
                var insertedUser = userManager.FindByName(user.UserName);
                userManager.AddToRole(insertedUser.Id, role);
            }
        }

        public void SaveGymClass(GymClass gymClass)
        {
            Context.GymClasses.Add(gymClass);

            Context.SaveChanges();
        }

        public void SaveSchedule(GymClassSchedule gymClassSchedule)
        {
            var store = new UserStore<FitAppUser>(Context);
            var userManager = new UserManager<FitAppUser>(store);

            gymClassSchedule.Trainer = userManager.FindById(gymClassSchedule.UserId);
            gymClassSchedule.Class = Context.GymClasses.FirstOrDefault(gc => gc.GymClassId == gymClassSchedule.GymClassId);

            Context.GymClassSchedules.Add(gymClassSchedule);

            Context.Entry(gymClassSchedule.Trainer).State = EntityState.Unchanged;
            Context.Entry(gymClassSchedule.Class).State = EntityState.Unchanged;

            Context.SaveChanges();
        }

        void IFitAppManager.SaveAppointment(Appointment appointment)
        {
            var store = new UserStore<FitAppUser>(Context);
            var userManager = new UserManager<FitAppUser>(store);

            appointment.Client = userManager.FindById(appointment.UserId);
            appointment.ClassSchedule = this.GetGymClassSchedule(appointment.ScheduleId);

            Context.Appointments.Add(appointment);

            Context.Entry(appointment.Client).State = EntityState.Unchanged;
            Context.Entry(appointment.ClassSchedule).State = EntityState.Unchanged;

            Context.SaveChanges();
        }

        void IFitAppManager.SaveSubscription(GymSubscription gymSubscription)
        {
            var store = new UserStore<FitAppUser>(Context);
            var userManager = new UserManager<FitAppUser>(store);

            gymSubscription.Client = userManager.FindById(gymSubscription.UserId);

            Context.GymSubscriptions.Add(gymSubscription);

            Context.Entry(gymSubscription.Client).State = EntityState.Unchanged;

            Context.SaveChanges();
        }

        void IFitAppManager.SaveLoginHistory(UserLoginHistory logHistory)
        {
            var existing = Context.UserHistory.SingleOrDefault(s => s.UserId == logHistory.UserId);

            if (existing != null)
            {
                existing.LastLoggenOn = logHistory.LastLoggenOn;
            }
            else
            {
                Context.UserHistory.Add(logHistory);
            }
            Context.SaveChanges();

        }

        void IFitAppManager.SaveNotifications(List<Notification> list)
        {
            Context.Notifications.AddRange(list);

            Context.SaveChanges();
        }




    }
}
