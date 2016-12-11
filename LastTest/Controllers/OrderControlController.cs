using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LastTest.Models;

namespace LastTest.Controllers
{
    public class OrderControlController : Controller
    {
        CoffeeServicesEntities db = new CoffeeServicesEntities();
        List<OrderInfo> listorder = new List<OrderInfo>();
        // GET: OrderControl
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListOrder(int? id)
        {
            List<StatusData> sdl = new List<StatusData>();
            sdl.Add(new StatusData { ID = 1, Name = "REQUEST" });
            sdl.Add(new StatusData { ID = 2, Name = "CONFIRM" });
            sdl.Add(new StatusData { ID = 3, Name = "SHIPPING" });
            sdl.Add(new StatusData { ID = 4, Name = "DELIVERED" });
            var sli = new SelectList(sdl, "ID", "Name");

            ViewBag.IdStatus = sli;
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
                               us.DisplayName
                           };

                foreach (var item in list)
                {
                    listorder.Add(new OrderInfo(item.ID, item.IDCustomer, item.IDShip, item.Date, item.Status,
                        item.DisplayName));
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
                               us.DisplayName
                           };

                foreach (var item in list)
                {
                    listorder.Add(new OrderInfo(item.ID, item.IDCustomer, item.IDShip, item.Date, item.Status,
                        item.DisplayName));
                }



                return View(listorder);
            }
            
        }
        [HttpPost]
        public ActionResult Reload(int? IdStatus)
        {
            return RedirectToAction("ListOrder", new { id = IdStatus });
        }

        private class StatusData
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

    }
}