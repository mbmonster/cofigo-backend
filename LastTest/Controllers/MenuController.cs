using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LastTest.Models;

namespace LastTest.Controllers
{
    public class MenuController : ApiController
    {
        IMenuManager menuManager = new MenuManager();
        [HttpGet]
        public List<Menu> GetTopOffer()
        {
            return menuManager.GetTopOfferMenu();
        }

        [HttpGet]
        public List<Menu> GetTopSelled()
        {
            return menuManager.GetTopSellMenu();
        } 
    }
}
