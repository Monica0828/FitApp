using FitApp.Models;
using System.Collections.Generic;

namespace FitApp.Interfaces
{
    public interface IFitAppManager
    {
        void RegisterUser(FitAppUser user, string password);

        object GetUserManager(Login loginCredentials);

        void Save(GymClass employee);

        GymClass Get(int id);

        IList<GymClass> GetAll();
    }
}