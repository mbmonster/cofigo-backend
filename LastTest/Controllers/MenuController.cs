using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LastTest.Models;
using Microsoft.Owin.Security.DataHandler.Encoder;

namespace LastTest.Controllers
{
    public class MenuController : Controller
    {
        CoffeeServicesEntities db = new CoffeeServicesEntities();
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListMenu(int? id)
        {
            ViewBag.IdStore = new SelectList(db.Stores, "ID", "NameStore");
            if (id == null)
            {
                var listMenu = (from a in db.Menus
                    join b in db.Stores on a.IDStore equals b.ID
                    select new
                    {
                        a.ID,
                        a.Name,
                        a.Price,
                        a.OfferPercent,
                        a.Selled,
                        a.IDStore,
                        b.NameStore,
                        a.Image
                    }).ToList();
                List<Listmenu> list = new List<Listmenu>();
                foreach (var item in listMenu)
                {

                    list.Add(new Listmenu(item.ID, item.Name, item.Price, item.OfferPercent, item.Selled, item.IDStore,
                        item.NameStore, item.Image));
                }

                return View(list);
            }
            else
            {
                var listMenu = (from a in db.Menus
                                join b in db.Stores on a.IDStore equals b.ID
                                where a.IDStore == id
                                select new 
                                {
                                    a.ID,
                                    a.Name,
                                    a.Price,
                                    a.OfferPercent,
                                    a.Selled,
                                    a.IDStore,
                                    b.NameStore,
                                    a.Image
                                }).ToList();
                List<Listmenu> list = new List<Listmenu>();
                foreach (var item in listMenu)
                {

                    list.Add(new Listmenu(item.ID, item.Name, item.Price, item.OfferPercent, item.Selled, item.IDStore,
                        item.NameStore, item.Image));
                }
                
                return View(list);
            }
        }

        [HttpPost]
        public ActionResult Reload(int? IdStore)
        {
            return RedirectToAction("ListMenu", new { id = IdStore });
        }
        public ActionResult EditMenu(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Listmenu menu = (from a in db.Menus
                            join b in db.Stores on a.IDStore equals b.ID
                            where a.ID == id
                            select new Listmenu
                            {
                                Id = a.ID,
                                Name = a.Name,
                                Price = a.Price,
                                OfferPercent = a.OfferPercent,
                                Selled = a.Selled,
                                IdStore = a.IDStore,
                                NameStore = b.NameStore,
                                Image = a.Image

                            }).First();
            return View(menu);
        }

        [HttpPost, ActionName("EditMenu")]
        [ValidateAntiForgeryToken]
        public ActionResult EditMenuPost(int? id)
        {
            var menuUpdate = db.Menus.Find(id);
            if (TryUpdateModel(menuUpdate, "", new string[] { "Name", "Price", "OfferPercent", "Selled","Image"}))
            {
                try
                {
                    db.Entry(menuUpdate).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Error Save Data");
                }
            }
            return RedirectToAction("ListMenu");
        }

        public ActionResult DeleteMenu(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Listmenu menu = (from a in db.Menus
                             join b in db.Stores on a.IDStore equals b.ID
                             where a.ID == id
                             select new Listmenu
                             {
                                 Id = a.ID,
                                 Name = a.Name,
                                 Price = a.Price,
                                 OfferPercent = a.OfferPercent,
                                 Selled = a.Selled,
                                 IdStore = a.IDStore,
                                 NameStore = b.NameStore,
                                 Image = a.Image
                             }).First();
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }
        [HttpPost, ActionName("DeleteMenu")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteMenuPost(int? id)
        {
            Menu menu = db.Menus.Find(id);
            db.Menus.Remove(menu);
            db.SaveChanges();
            return RedirectToAction("ListMenu");
        }
        // tao moi menu co su dung dropdown list
        public ActionResult AddMenu()
        {
            ViewBag.IdStore = new SelectList(db.Stores, "ID", "NameStore");
            return View();
        }

        [HttpPost, ActionName("AddMenu")]
        [ValidateAntiForgeryToken]
        public ActionResult AddMenu(Menu menu)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    db.Menus.Add(menu);
                    db.SaveChanges();
                    return RedirectToAction("ListMenu");
                }
                return View(menu);
            }
            catch (Exception)
            {
                return View();
                throw;
            }

        }
    }
}