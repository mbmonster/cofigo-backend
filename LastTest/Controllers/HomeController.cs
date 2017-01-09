using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LastTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("Index");
        }
        public ActionResult Signin()
        {
            return View("Index");
        }
        public ActionResult Menu()
        {
            return View("Index");
        }
        public ActionResult Stores()
        {
            return View("Index");
        }
        public ActionResult Store(int id)
        {
            return View("Index");
        }
        public ActionResult Order(string id)
        {
            return View("Index");
        }
        public ActionResult Search()
        {
            return View("Index");
        }
    }
}
