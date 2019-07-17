using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _4S.WebUI.Controllers
{
    public class PartialController : Controller
    {
        // GET: Partial
        public ActionResult Index()
        {
            return View();
        }
        //[ChildActionOnly]
        public PartialViewResult ChildAction(DateTime time)
        {
            string greetings = time.ToString();
            return PartialView("ChildAction", greetings);
        }
    }
}