using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LastTest.Models;
using Microsoft.AspNet.Identity;

namespace LastTest.Controllers
{
    [System.Web.Http.HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
    [Authorize(Roles = "Admin")]
    public class OrderControlController : Controller
    {

        CoffeeServicesEntities db = new CoffeeServicesEntities();
        List<OrderInfo> listorder = new List<OrderInfo>();
        // GET: OrderControl
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult ListOrder(int? id,string sortOrder)
        {
            ViewBag.IdStatus = new SelectList(new[] { "REQUEST", "CONFIRM", "SHIPPING", "DELIVERED" });
            ViewBag.Current = "ListOrder";
            ViewBag.StatusSort = String.IsNullOrEmpty(sortOrder) ? "Status" : "";
            ViewBag.DateSort = sortOrder == "Date" ? "Status" : "Date";

            List<StatusData> sdl = new List<StatusData>();
            sdl.Add(new StatusData { ID = 1, Name = "REQUEST" });
            sdl.Add(new StatusData { ID = 2, Name = "CONFIRM" });
            sdl.Add(new StatusData { ID = 3, Name = "SHIPPING" });
            sdl.Add(new StatusData { ID = 4, Name = "DELIVERED" });
            ViewBag.Status = new SelectList(sdl,"ID","Name");
            if (id == null)
            {
                var list = from od in db.Orders
                    join us in db.Users on od.IDCustomer equals us.ID
                    select new
                           {
                               od.ID,
                               od.IDCustomer,
                               od.IDShip,
                               od.Date,
                               od.Status,
                               us.DisplayName,
                               od.SDT
                           };
                switch (sortOrder)
                {
                    case "Status":
                        list = list.OrderByDescending(s => s.Status);
                        break;
                    case "Date":
                        list = list.OrderByDescending(s => s.Date);
                        break;
                    default:
                        list = list.OrderBy(s => s.Status);
                        break;
                }
                foreach (var item in list)
                {
                    listorder.Add(new OrderInfo(item.ID, item.IDCustomer, item.IDShip, item.Date, item.Status,item.DisplayName,item.SDT));
                }
                return View(listorder);
            }
            else
            {
                string status;
                switch (id)
                {
                    case 1:
                        status = "REQUEST";
                        break;
                    case 2:
                        status = "CONFIRM";
                        break;
                    case 3:
                        status = "SHIPPING";
                        break;
                    case 4:
                        status = "DELIVERED";
                        break;
                    default: 
                        status = "REQUEST";
                        break;
                }
                var list = from od in db.Orders
                           join us in db.Users on od.IDCustomer equals us.ID where od.Status==status
                           select new
                           {
                               od.ID,
                               od.IDCustomer,
                               od.IDShip,
                               od.Date,
                               od.Status,
                               us.DisplayName,
                               od.SDT
                           };
                
                foreach (var item in list)
                {
                    listorder.Add(new OrderInfo(item.ID, item.IDCustomer, item.IDShip, item.Date, item.Status,
                        item.DisplayName, item.SDT));
                }

                

                return View(listorder);
            }

            
        }
        [HttpPost]
        public ActionResult Reload(int? Status)
        {
            return RedirectToAction("ListOrder", new { id = Status });
        }
        private class StatusData
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        public ActionResult UpdateOrder(string id)
        {
            
            OrderInfo list = (from od in db.Orders
                       join us in db.Users on od.IDCustomer equals us.ID
                       where od.ID==id
                       select new OrderInfo
                       {
                            ID = od.ID,
                            Date = (DateTime)od.Date,
                            DisplayName = us.DisplayName,
                          IDCustomer = od.IDCustomer,
                          IDShip = (int)od.IDShip,
                          Status = od.Status
                       }).First();
           
            return View(list);
        }
        [HttpPost]
        public ActionResult UpdateOrder(OrderInfo form, string id)
        {
            
            var order = db.Orders.First(m => m.ID == id);
            if (TryUpdateModel(order, "", new string[] { "ID", "Date", "DisplayName", "IDCustomer", "IDShip", "Status" }))
            {
                try
                {
                    db.Entry(order).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Error Save Data");
                }
            }
            return RedirectToAction("ListOrder");
        }

        public ActionResult Sorting(string sortOrder)
        {
            ViewBag.StatusSort = String.IsNullOrEmpty(sortOrder) ? "Status" : "";
            ViewBag.DateSort = sortOrder == "Date" ? "Status" : "Date";
            var list = from od in db.Orders
                       join us in db.Users on od.IDCustomer equals us.ID
                       select new
                       {
                           od.ID,
                           od.IDCustomer,
                           od.IDShip,
                           od.Date,
                           od.Status,
                           us.DisplayName,
                           od.SDT
                       };

            foreach (var item in list)
            {
                listorder.Add(new OrderInfo(item.ID, item.IDCustomer, item.IDShip, item.Date, item.Status,
                    item.DisplayName, item.SDT));
            }
            
            switch (sortOrder)
            {
                case "Status":
                    list = list.OrderByDescending(s => s.Status);
                    break;
                case "Date":
                    list = list.OrderBy(s => s.Date);
                    break;
                default:
                    list = list.OrderBy(s => s.Status);
                    break;
            }
            return View(list.ToList());
        }
    }
}