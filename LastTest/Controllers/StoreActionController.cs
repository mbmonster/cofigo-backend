using System;
using System.Collections.Generic;
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
namespace LastTest.Controllers
{
    
    public class StoreActionController : Controller
    {
        CoffeeServicesEntities db = new CoffeeServicesEntities();
        // GET: StoreAction
        public ActionResult Index()
        {
            return View();
        }

        [System.Web.Http.HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [Authorize(Roles = "Admin")]
        public ActionResult ViewStore()
        {
            
            var list = (from s in db.Stores
                select s);
            return View(list);
        }

        public ActionResult StoreDetails()
        {
            var storedetails = (from s in db.Stores select s).ToList();
            return View(storedetails);
        }
     
        public ActionResult AddStore()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddStore(Store form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    db.Stores.Add(form);
                    db.SaveChanges();
                    return View("StoreDetails");
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
            return RedirectToAction("ViewStore");
        }

        public ActionResult EditStore(int id)
        {
            var store = db.Stores.First(m => m.ID==id);
            return View(store);
        }
        [HttpPost]
        public ActionResult EditStore(FormCollection form, int id)
        {
            var store = db.Stores.First(m => m.ID == id);
            string namestore = form["NameStore"];
            string address = form["Address"];
            string avatar = form["Avatar"];
            string cover = form["Cover"];
            int rep = Convert.ToInt32(form["Rep"]);
            double lat = Convert.ToDouble(form["Latitude"]);
            double longt = Convert.ToDouble(form["Longtitude"]);
            string moblie = form["Mobile"];
            store.NameStore = namestore;
            store.Address = address;
            store.Avatar = avatar;
            store.Cover = cover;
            store.Rep = rep;
            store.Latitude = lat;
            store.Longtitude = longt;
            store.Mobile = moblie;
            UpdateModel(store);
            db.SaveChanges();
            return RedirectToAction("StoreDetails");
        }
    }
}