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

        public HttpResponseMessage AddOrder(List<OrderDetail> list, string ID, int? IDShip, string IDCustomer, string SDT, double? Latitude, double? Longtitude)
        {
            if (IDShip == null || IDCustomer == null || list == null || ID == null || SDT==null || Latitude == null || Longtitude ==null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            
            }
            double? total = 0;
            foreach (var item in list)
            {
               var menu = db.Menus.FirstOrDefault(m => m.ID == item.IDMenu);
               if (menu.OfferPercent != null)
               {
                   total = total + menu.Price * item.Quantity * (100 - menu.OfferPercent)*100;
               }
               else
               {
                   total = total + menu.Price * item.Quantity;
               }
               
            }
            
            var shipPrice = db.Ships.FirstOrDefault(m => m.ID == IDShip);
            if (shipPrice.OfferPercent != null)
            {
                total = total + shipPrice.Price * (100 - shipPrice.OfferPercent)/100;
            }
            else
            {
                total = total + shipPrice.Price;
            }
            
            DateTime localDate = DateTime.Now;
            db.Orders.Add(new Order {ID = ID, IDShip = IDShip, IDCustomer = IDCustomer, Date = localDate,SDT = SDT,Latitude = Latitude,Longtitude = Longtitude, Status = "REQUEST",Total = total });
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
                var exMes = ex;
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
 
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
      
        

       
    }
}