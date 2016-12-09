using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
        public ActionResult EditUser(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.SingleOrDefault(s => s.ID == id);
            return View(user);
        }

        [HttpPost, ActionName("EditUser")]
        [ValidateAntiForgeryToken]
        public ActionResult EditUserPost(string id)
        {
            var userUpdate = db.Users.Find(id);
            if (TryUpdateModel(userUpdate, "", new string[] { "ID", "Account", "Role", "DisplayName" }))
            {
                try
                {
                    db.Entry(userUpdate).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Error Save Data");
                }
            }
            return RedirectToAction("ListUser");
        }

        public ActionResult DeleteUser(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUserPost(string id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("ListUser");
        }
    }
}