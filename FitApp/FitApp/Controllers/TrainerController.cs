using FitApp.BusinessLogic;
using FitApp.Interfaces;
using FitApp.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FitApp.Controllers
{
    public class TrainerController : Controller
    {
        private readonly IFitAppManager manager;

        public TrainerController()
        {
            manager = new SqlFitAppManager();
        }

        public ActionResult SeeClasses()
        {
            var classes = manager.GetAllClasses();

            return View(classes);
        }

        [HttpGet]
        public ActionResult AddClass()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddClass(GymClass gymClass)
        {
            gymClass.DateCreated = DateTime.Now;
            manager.SaveGymClass(gymClass);

            return RedirectToAction(nameof(AddClass));

        }

        [HttpGet]
        public ActionResult CreateSchedule()
        {
            var classes = manager.GetAllClasses();

            List<SelectListItem> allClasses = new List<SelectListItem>();

            foreach (var c in classes)
            {
                allClasses.Add(new SelectListItem
                {
                    Text = c.Name,
                    Value = c.GymClassId.ToString()
                });
            }
            ViewBag.Classes = allClasses;
            return View();
        }

        [HttpPost]
        public ActionResult CreateSchedule(GymClassSchedule gymClassSchedule)
        {
            gymClassSchedule.UserId = User.Identity.GetUserId();

            manager.SaveSchedule(gymClassSchedule);

            return RedirectToAction("CreateSchedule", "Trainer");
        }

        [HttpGet]
        public ActionResult SeeSchedules()
        {
            var list = manager.GetTrainerSchedule(User.Identity.GetUserId());
            return View(list);
        }

        [HttpGet]
        public ActionResult EditSchedule(int id)
        {
            var model = manager.GetGymClassSchedule(id);
            return View(model);
        }
    }
}