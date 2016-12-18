using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public ActionResult StoreDetails(int page =1 , int pageSize=5)
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
        [System.Web.Mvc.HttpPost]
        public ActionResult AddStore(Store form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    form.Rep = 0;
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
        
        public ActionResult DeleteStore(int id)
        {
            var store = db.Stores.First(m => m.ID == id);
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
        public ActionResult EditStore(Store form, int id)
        {
            var store = db.Stores.First(m => m.ID == id);
            string namestore = form.NameStore;
            string address = form.Address;
            string avatar = form.Avatar;
            string cover = form.Cover;
            //int rep = Convert.ToInt32(form.Rep);
            double lat = Convert.ToDouble(form.Latitude);
            double longt = Convert.ToDouble(form.Longtitude);
            string moblie = form.Mobile;
            store.NameStore = namestore;
            store.Address = address;
            store.Avatar = avatar;
            store.Cover = cover;
            store.Latitude = lat;
            store.Longtitude = longt;
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