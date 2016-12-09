using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
                item.Date = DateTime.Now;
            }
            
        }

        public List<Order> GetOrder()
        {
            
            return listoOrders.ToList();
        }
    }
}