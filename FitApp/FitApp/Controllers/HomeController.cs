using FitApp.BusinessLogic;
using FitApp.Interfaces;
using System.Web.Mvc;

namespace FitApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFitAppManager  manager;

        public HomeController()
        {
            manager = new SqlFitAppManager();
        }

        public ActionResult Index()
        {
            var classes = manager.GetAllClasses();

            return View(classes);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Chart()
        {
            return View();
        }
    }
}