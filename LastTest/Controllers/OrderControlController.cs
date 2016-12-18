using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LastTest.Models;
using Microsoft.AspNet.Identity;
using PagedList;

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
        public ActionResult ListOrder(int? id,string sortOrder, int page =1 , int pageSize=2)
        {
            //ViewBag.IdStatus = new SelectList(new[] { "REQUEST", "CONFIRM", "SHIPPING", "DELIVERED" }); // search bằng dropdown list
            ViewBag.Current = "ListOrder"; // để follow theo navpill
#region[Sắp xếp order theo status và date, truyền vào 1 string "sortOrder"nếu như là status hoặc date thì switch case "sortOrder" ở dưới ]
            ViewBag.StatusSort = String.IsNullOrEmpty(sortOrder) ? "Status" : "";
            ViewBag.DateSort = sortOrder == "Date" ? "Status" : "Date";
#endregion
            
            //truyền vào statusdata, sau đó truyền vào viewbag tên Status, qua bên dropdownlist nhận vào cái viewbag
            List<StatusData> sdl = new List<StatusData>();
            sdl.Add(new StatusData { ID = 1, Name = "REQUEST" });
            sdl.Add(new StatusData { ID = 2, Name = "CONFIRM" });
            sdl.Add(new StatusData { ID = 3, Name = "SHIPPING" });
            sdl.Add(new StatusData { ID = 4, Name = "DELIVERED" });
            ViewBag.Status = new SelectList(sdl,"ID","Name");
            //nếu không có id thì show bình thường
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
                //xắp xếp
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
                //paging 
                PagedList<OrderInfo> order = new PagedList<OrderInfo>(listorder,page,pageSize);
                return View(order);
            }
                //ngược lại thì show cái nào có status trùng id
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
                PagedList<OrderInfo> order = new PagedList<OrderInfo>(listorder, page, pageSize);
               

                return View(order);
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
            //cái này dùng để đổ dữ liệu vào form để sửa
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

       
    }
}