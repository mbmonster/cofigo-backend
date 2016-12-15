using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace LastTest.Models
{
    public class OrderManager : IOrderManager
    {
        CoffeeServicesEntities db = new CoffeeServicesEntities();
        List<Order> listoOrders = new List<Order>();

        public OrderManager()
        {
            var list = from s in db.Orders select s;
            foreach (var item in list)
            {
                listoOrders.Add(new Order
                {
                    Date = item.Date,
                    IDShip = item.IDShip,
                    IDCustomer = item.IDCustomer,
                    Status = item.Status
                });
               
            }
            
        }

        public HttpResponseMessage AddOrder(List<OrderDetail> list, string ID, int IDShip, string IDCustomer)
        {
            if (IDShip == null || IDCustomer == null || list == null || ID == null )
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            
            }
            DateTime localDate = DateTime.Now;
            db.Orders.Add(new Order {ID = ID, IDShip = IDShip, IDCustomer = IDCustomer, Date = localDate, Status = "REQUEST" });
            foreach (var item in list)
            {
                item.IDOrder = ID;
                db.OrderDetails.Add(item);
            }
            try
            {
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
 
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
      
        

       
    }
}