using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace LastTest.Controllers
{
    public class StoreMenuActionController : Controller
    {
        // GET: StoreMenuAction
        CoffeeServicesEntities db = new CoffeeServicesEntities();
        public ActionResult GetListStore(string searchString)
        {
            var store = from s in db.Stores
                select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                store = store.Where(s => s.NameStore.Contains(searchString) || s.Address.Contains(searchString));
            }
            return View(store);
        }
        public ActionResult DetailMenu(int? idStore, string sortString, string currentFilter, string searchString, int? page, string type)
        {
            ViewBag.CurrentSort = sortString;
            ViewBag.PriceSort = "Price";
            ViewBag.SelledSort = "Selled";
            if (sortString != "Price" && sortString != "Selled")
            {
                ViewBag.NameSort = String.IsNullOrEmpty(sortString) ? "Name" : "";
            }
            if (String.IsNullOrEmpty(type))
            {
                ViewBag.Type = "desc";
            }
            if (idStore == null)
            {
                return HttpNotFound();
            }
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var listMenu = from m in db.Menus
                where m.IDStore == idStore
                select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                listMenu = listMenu.Where(m => m.Name.Contains(searchString));
            }
            ViewBag.IdStore = idStore;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            if (sortString != null)
            {
                switch (sortString)
                {
                    case "Name":
                        listMenu = listMenu.OrderByDescending(m => m.Name);
                        break;
                    case "Price":
                        if (type == "desc")
                        {
                            listMenu = listMenu.OrderByDescending(m => m.Price);
                        }
                        else
                        {
                            listMenu = listMenu.OrderBy(m => m.Price);
                        }
                        break;
                    case "Selled":
                        if (type == "desc")
                        {
                            listMenu = listMenu.OrderByDescending(m => m.Selled);
                        }
                        else
                        {
                            listMenu = listMenu.OrderBy(m => m.Selled);
                        }
                        break;
                    case "id":
                        listMenu = listMenu.OrderByDescending(m => m.ID);
                        break;
                }
   
            }
            else
            {
                listMenu = listMenu.OrderBy(m => m.Name);
            }
            
            return View(listMenu.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult AddMenu()
        {
            return View();
        }

        [HttpPost, ActionName("AddMenu")]
        [ValidateAntiForgeryToken]
        public ActionResult AddMenu(Menu menu, HttpPostedFileBase image)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var filename = image.FileName;
                    string filePathOriginal = System.Web.Hosting.HostingEnvironment.MapPath("~/Content/Uploads/Menus");
                    string savedFileName = Path.Combine(filePathOriginal, filename);
                    image.SaveAs(savedFileName);
                    menu.Image = "http://localhost:18179/Content/Uploads/Menus/" + filename;
                    db.Menus.Add(menu);
                    db.SaveChanges();
                    return RedirectToAction("DetailMenu", new {idStore = menu.IDStore,sortString = "id"});
                }
                return View(menu);
            }
            catch (Exception)
            {
                return View();
                throw;
            }

        }
        public ActionResult EditMenu(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.SingleOrDefault(s => s.ID == id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        [HttpPost, ActionName("EditMenu")]
        [ValidateAntiForgeryToken]
        public ActionResult EditMenuPost(int? id)
        {
            var menuUpdate = db.Menus.Find(id);
            if (TryUpdateModel(menuUpdate, "", new string[] { "Name", "Price", "OfferPercent", "Selled", "Image" }))
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
            return RedirectToAction("DetailMenu", new { idStore = menuUpdate.IDStore });
        }
        public ActionResult DeleteMenu(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.SingleOrDefault(s => s.ID == id);
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
            int? idS = menu.IDStore;
            db.Menus.Remove(menu);
            db.SaveChanges();
            return RedirectToAction("DetailMenu", new { idStore = idS});
        }
        
    }
}