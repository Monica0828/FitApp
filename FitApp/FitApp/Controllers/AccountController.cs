using FitApp.BusinessLogic;
using FitApp.Interfaces;
using FitApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
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
                return RedirectToAction("Index", "Home");
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

            _repository.RegisterUser(user, registerModel.Password, registerModel.UserType.ToString());

            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();

            var logHistory = new UserLoginHistory()
            {
                UserId = User.Identity.GetUserId(),
                LastLoggenOn = DateTime.Now
            };

            _repository.SaveLoginHistory(logHistory);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public JsonResult SendNotification()
        {
            var loggedOn = _repository.GetLastLoggenOn(User.Identity.GetUserId());

            if (loggedOn != DateTime.MinValue)
            {
                var list = _repository.GetNewGymClasses(loggedOn, User.Identity.GetUserId()).Select(gc => new Notification
                {
                    UserId = User.Identity.GetUserId(),
                    Message = $"New class {gc.Name.ToLower()} was added",
                    GymClassId= gc.GymClassId,
                    Seen = false
                }).ToList();

                _repository.SaveNotifications(list);
            }

            var OfNotification= _repository.GetNotifications(User.Identity.GetUserId());
            return Json(OfNotification, JsonRequestBehavior.AllowGet);
            //return _repository.GetNotifications(User.Identity.GetUserId());
            ///return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public void MarkNotificationsAsRead()
        {
            var list = _repository.GetNotifications(User.Identity.GetUserId());

            _repository.MarkNotificationsAsRead(list);
        }
    }
}