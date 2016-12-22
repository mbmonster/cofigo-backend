using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LastTest.Controllers
{
    public class PromotionActionController : Controller
    {
        CoffeeServicesEntities db = new CoffeeServicesEntities();
        public ActionResult GetListProm()
        {
            var prom = from s in db.Promotions
                        select s;
            return View(prom);
        }
        public ActionResult AddProm()
        {
            return View();
        }

        [HttpPost, ActionName("AddProm")]
        [ValidateAntiForgeryToken]
        public ActionResult AddProm(Promotion prom)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    db.Promotions.Add(prom);
                    db.SaveChanges();
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("","Error save data");
            }
            var listProm = from s in db.Promotions
                       select s;
            return View("GetListProm", listProm);
        }
        public ActionResult EditProm(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promotion prom = db.Promotions.SingleOrDefault(s => s.ID == id);
            if (prom == null)
            {
                return HttpNotFound();
            }
            return View(prom);
        }

        [HttpPost, ActionName("EditProm")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPromPost(int? id)
        {
            var promUpdate = db.Promotions.Find(id);
            if (TryUpdateModel(promUpdate, "", new string[] { "Title", "Description", "Image", "Created", "Last" }))
            {
                try
                {
                    db.Entry(promUpdate).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Error Save Data");
                }
            }
            return RedirectToAction("GetListProm");
        }
        public ActionResult DeleteProm(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promotion prom = db.Promotions.SingleOrDefault(s => s.ID == id);
            if (prom == null)
            {
                return HttpNotFound();
            }
            return View(prom);
        }
        [HttpPost, ActionName("DeleteProm")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePromPost(int? id)
        {
            Promotion prom = db.Promotions.Find(id);
            db.Promotions.Remove(prom);
            db.SaveChanges();
            return RedirectToAction("GetListProm");
        }
    }
}