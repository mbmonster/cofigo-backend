using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LastTest.Controllers
{
    public class AdminController : Controller
    {
        CoffeeServicesEntities db = new CoffeeServicesEntities();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListUser()
        {
            var listUser = db.Users.ToList();
            return View(listUser);
        }
    }
}