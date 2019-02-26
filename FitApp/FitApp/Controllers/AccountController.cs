using FitApp.BusinessLogic;
using FitApp.Interfaces;
using FitApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Web;
using System.Web.Mvc;

namespace FitApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IFitAppManager _repository;
        public AccountController()
        {
            _repository = new SqlFitAppManager();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login login)
        {
            var userManager = _repository.GetUserManager(login) as UserManager<FitAppUser>;
            var user = userManager.Find(login.Email, login.Password);

            if (user != null)
            {
                var authenticationManager = HttpContext.GetOwinContext().Authentication;
                var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

                authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, userIdentity);
                return RedirectToAction(nameof(login));
            }

            return RedirectToAction(nameof(login));
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Register registerModel)
        {
            var user = new FitAppUser
            {
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName,
                UserName = registerModel.Email,
                Email = registerModel.Email,
                Birthdate = DateTime.Now
            };

            _repository.RegisterUser(user, registerModel.Password);

            return RedirectToAction(nameof(Login));
        }
    }
}