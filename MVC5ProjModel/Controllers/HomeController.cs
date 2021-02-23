using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5ProjModel.Controllers
{
    public class HomeController : Controller
    {
        [HandleError(ExceptionType = typeof(Exception), View = "MyError")]
        public ActionResult Index()
        {
            //throw new Exception("this is a Error Page");
            return View();
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
    }
}