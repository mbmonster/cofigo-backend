﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LastTest.Models;
using Newtonsoft.Json;

namespace LastTest.Controllers
{
    public class OrderController : ApiController
    {
        IOrderManager orderManager = new OrderManager();

        public List<Order> GetOrders()
        {
            return orderManager.GetOrder();
        }

   
        public HttpResponseMessage AddOrder([FromBody] Dictionary<string,string> value)
        {
            List<OrderDetail> res = (List<OrderDetail>)JsonConvert.DeserializeObject(value["arrayMenu"], typeof(List<OrderDetail>));
            
            return orderManager.AddOrder(res, value["ID"],Convert.ToInt32(value["IDShip"]), "1");
             
        }
    }
}