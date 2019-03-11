using FitApp.BusinessLogic;
using FitApp.Interfaces;
using FitApp.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FitApp.Controllers
{
    public class ClientController : Controller
    {
        private readonly IFitAppManager manager;

        public ClientController()
        {
            manager = new SqlFitAppManager();
        }
        [HttpGet]
        public ActionResult AddAppointment()
        {
            var list = manager.GetSchedule(User.Identity.GetUserId());

            return View(list);
        }

        [HttpPost]
        public ActionResult AddAppointment(int scheduleId)
        {
            var appointment = new Appointment();

            appointment.ScheduleId = scheduleId;
            appointment.UserId = User.Identity.GetUserId();

            manager.SaveAppointment(appointment);

            return RedirectToAction(nameof(AddAppointment));
        }

        [HttpGet]
        public ActionResult SeeAppointments()
        {
            var list = manager.GetAllAppointment(User.Identity.GetUserId());

            return View(list);
        }

        [HttpGet]
        public ActionResult AddSubscription()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSubscription(GymSubscription gymSubscription)
        {
            gymSubscription.UserId = User.Identity.GetUserId();

            manager.SaveSubscription(gymSubscription);

            return RedirectToAction(nameof(AddSubscription));
        }

        [HttpGet]
        public ActionResult SeeSubscriptions()
        {
            var list = manager.GetSubscriptions(User.Identity.GetUserId());

            return View(list);
        }
    }
}