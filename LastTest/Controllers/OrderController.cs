using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using LastTest.Models;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;
using System.Web.Http.Cors;

namespace LastTest.Controllers
{
    [System.Web.Http.Authorize]
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class OrderController : ApiController
    {
        IOrderManager orderManager = new OrderManager();

        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        public HttpResponseMessage AddOrder([FromBody] OrderReceivedModel data)
        {
           
            string user = User.Identity.GetUserName();
            List<OrderDetail> res = (List<OrderDetail>)JsonConvert.DeserializeObject(data.arrayMenu, typeof(List<OrderDetail>));
            return orderManager.AddOrder(res, data.ID, data.IDShip, user, data.SDT, data.Latitude, data.Longtitude);
           
        }
    }
}
