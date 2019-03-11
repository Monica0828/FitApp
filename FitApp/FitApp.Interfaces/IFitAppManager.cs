using FitApp.Models;
using System;
using System.Collections.Generic;

namespace FitApp.Interfaces
{
    public interface IFitAppManager
    {
        void RegisterUser(FitAppUser user, string password, string role);

        object GetUserManager(Login loginCredentials);

        IList<GymClassSchedule> GetSchedule(string clientId);

        IList<GymSubscription> GetSubscriptions(string clientId);

        IList<GymClass> GetAllClasses();

        IList<Appointment> GetAllAppointment(string clientId);

        IList<int> GetScheduleIdForClient(string clientId);

        IList<GymClassSchedule> GetTrainerSchedule(string trainerId);

        DateTime GetLastLoggenOn(string userId);

        List<GymClass> GetNewGymClasses(DateTime loggedOn, string clientId);

        List<Notification> GetNotifications(string clientId);

        void MarkNotificationsAsRead(List<Notification> list);

        GymClassSchedule GetGymClassSchedule(int scheduleId);

        void SaveGymClass(GymClass gymClass);

        void SaveSchedule(GymClassSchedule gymClassSchedule);

        void SaveAppointment(Appointment appointment);

        void SaveSubscription(GymSubscription gymSubscription);

        void SaveLoginHistory(UserLoginHistory logHistory);

        void SaveNotifications(List<Notification> lista);

        GymClass Get(int id);

        
    }
}