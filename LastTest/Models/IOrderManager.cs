using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LastTest.Models
{
    interface IOrderManager
    {
        List<Order> GetOrder();
        HttpResponseMessage AddOrder(List<OrderDetail> list, string ID, int IDShip,string IDCustomer);
    }
}
