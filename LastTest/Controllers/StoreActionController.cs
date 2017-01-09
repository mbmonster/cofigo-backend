using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Threading;
using System.Web.Http;
using PagedList;

namespace LastTest.Controllers
{
    [System.Web.Http.HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
    [System.Web.Mvc.Authorize(Roles = "Admin")]   
    public class StoreActionController : Controller
    {
        CoffeeServicesEntities db = new CoffeeServicesEntities();
        // GET: StoreAction
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult ViewStore()
        {
            ViewBag.Current = "ViewStore";
            var list = (from s in db.Stores
                select s);
            return View(list);
            
        }

        public ActionResult StoreDetails(int page =1 , int pageSize=12)
        {
            ViewBag.Current = "StoreDetails";
            var storedetails = (from s in db.Stores select s).ToList();
            //int index;
            //if (page==null)
            //{
            //    index = 0;
            //}
            //else
            //{
            //    index = (Convert.ToInt32(page) - 1) * 5;
            //}
            PagedList<Store> stores = new PagedList<Store>(storedetails, page,pageSize);
                
            return View(stores);
            
        }
     
        public ActionResult AddStore()
        {
            ViewBag.Current = "AddStore";
            return View();
        }
        [ValidateAntiForgeryToken]
        [System.Web.Mvc.HttpPost]
        public ActionResult AddStore(Store form, HttpPostedFileBase file, HttpPostedFileBase cover)
        {

            
            try
            {
                string pathAvatar = "";
                string extension = "";
                string pathCover = "";
                string extensionCover = "";
                if (file != null || cover!=null)
                {

                     extension = Path.GetFileName(file.FileName);
                     pathAvatar = Path.Combine(Server.MapPath("~/Content/Images"), extension);
                     file.SaveAs(pathAvatar);

                     extensionCover = Path.GetFileName(cover.FileName);
                     pathCover = Path.Combine(Server.MapPath("~/Content/Cover"), extensionCover);
                     cover.SaveAs(pathCover);

                }
                if (ModelState.IsValid)
                {
                    form.Rep = 0;
                    form.Avatar = ("http://localhost:18179/Content/Images/" + extension);
                    form.Cover = ("http://localhost:18179/Content/Cover/" + extensionCover);
                    db.Stores.Add(form);
                     
                    db.SaveChanges();
                    return RedirectToAction("StoreDetails");
                }
                return View(form);
            }
            catch (Exception)
            {
                return View();
                throw;
            }
                
            
        }
        
        public ActionResult ViewDeleteStore(int id)
        {
            Store store = (from s in db.Stores where s.ID == id select s).First();
                                                    
            return View(store);
        }
        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("ViewDeleteStore")]
        public ActionResult DeleteStore(int id)
        {
            Store store = db.Stores.Find(id);
            db.Stores.Remove(store);
            db.SaveChanges();
            return RedirectToAction("StoreDetails");
        }

        public ActionResult EditStore(int id)
        {
            var store = db.Stores.First(m => m.ID==id);
            return View(store);
        }
        [System.Web.Mvc.HttpPost]
        public ActionResult EditStore(Store form, int id, HttpPostedFileBase file, HttpPostedFileBase cover)
        {
            var store = db.Stores.First(m => m.ID == id);
            string pathAvatar = "";
            string extension = "";
            string pathCover = "";
            string extensionCover = "";
            
            if (file != null)
            {

                extension = Path.GetFileName(file.FileName);
                pathAvatar = Path.Combine(Server.MapPath("~/Content/Images"), extension);
                file.SaveAs(pathAvatar);
                string avatar = ("http://localhost:18179/Content/Images/" + extension);
                 store.Avatar = avatar;
            }
            if (cover != null)
            {
                extensionCover = Path.GetFileName(cover.FileName);
                pathCover = Path.Combine(Server.MapPath("~/Content/Cover"), extensionCover);
                cover.SaveAs(pathCover);
                string cover1 = ("http://localhost:18179/Content/Cover/" + extensionCover);
                store.Cover = cover1;
            }
            //double lat = Convert.ToDouble();
            //double longt = Convert.ToDouble(form.Longtitude);
            if (form.Latitude != null)
            {
                store.Latitude = form.Latitude;
            }
            if (form.Longtitude != null)
            {
                store.Longtitude = form.Longtitude;
            }
           
            string namestore = form.NameStore;
            string address = form.Address;
            string moblie = form.Mobile;
            store.NameStore = namestore;
            store.Address = address;
            store.Mobile = moblie;
            db.SaveChanges();
            return RedirectToAction("StoreDetails");
            //var stores = db.Stores.First(m => m.ID == id);
            //if (TryUpdateModel(stores, "", new string[] { "NameStore", "Address", "Avatar", "Cover", "Latitude", "Longtitude", "Mobile" }))
            //{
            //    try
            //    {
            //        db.Entry(stores).State = EntityState.Modified;
            //        db.SaveChanges();
            //    }
            //    catch (Exception)
            //    {
            //        ModelState.AddModelError("", "Error Save Data");
            //    }
            //}
            //return RedirectToAction("StoreDetails");
        }

       
    }
}