using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
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
        public ActionResult AddProm(Promotion prom, HttpPostedFileBase image)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var filename = image.FileName;
                    string  filePathOriginal= System.Web.Hosting.HostingEnvironment.MapPath("~/Content/Promotions");
                    string savedFileName = Path.Combine(filePathOriginal, filename);
                    image.SaveAs(savedFileName);
                    prom.Image = "http://localhost:18179/Content/Promotions/" + filename;
                    prom.Created = DateTime.Now;
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
        public ActionResult EditPromPost(int? id, HttpPostedFileBase file, Promotion promotion)
        {
            var prom1 = db.Promotions.First(m => m.ID == id);
            string pathImage = "";
            string extension = "";
            if (file != null)
            {
                extension = Path.GetFileName(file.FileName);
                pathImage = Path.Combine(Server.MapPath("~/Content/Uploads/Promotions"), extension);
                file.SaveAs(pathImage);
                string image = ("http://localhost:18179/Content/Uploads/Promotions/" + extension);
                prom1.Image = image;
            }

            string title = promotion.Title;
            string dess = promotion.Description;
            DateTime last = (DateTime) promotion.Last;
            prom1.Title = title;
            prom1.Description = dess;
            prom1.Last = last;
            db.SaveChanges();
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