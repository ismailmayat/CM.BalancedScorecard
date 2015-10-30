using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CM.BalancedScoreboard.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Indicators()
        {
            return View();
        }

        public ActionResult Projects()
        {
            return View();
        }

        public ActionResult Objectives(Guid projectId)
        {
            return View();
        }
    }
}
