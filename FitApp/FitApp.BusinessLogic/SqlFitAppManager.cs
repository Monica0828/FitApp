using System.Collections.Generic;
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

        public IList<GymClass> GetAll()
        {
            return Context.GymClasses.ToList();
        }

        public FitAppUser FindUser(Login loginCredentials)
        {
            var store = new UserStore<FitAppUser>(Context);
            var userManager = new UserManager<FitAppUser>(store);

            return userManager.Find(loginCredentials.Email, loginCredentials.Password);
        }

        public void RegisterUser(FitAppUser user, string password)
        {
            var store = new UserStore<FitAppUser>(Context);
            var userManager = new UserManager<FitAppUser>(store);

            var result = userManager.Create(user, password);

        }

        public void Save(GymClass employee)
        {
            throw new System.NotImplementedException();
        }

        public object GetUserManager(Login loginCredentials)
        {
            var store = new UserStore<FitAppUser>(Context);
            var userManager = new UserManager<FitAppUser>(store);
            return userManager;
        }
    }
}
