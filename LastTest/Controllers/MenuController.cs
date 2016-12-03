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
        public List<Menu> GetTopOffer([FromUri] string page)
        {
            var index = (Convert.ToInt32(page) - 1)*10;
            return menuManager.GetTopOfferMenu(index);
        }

        [HttpGet]
        public List<Menu> GetTopSelled([FromUri] string page)
        {
            var index = (Convert.ToInt32(page) - 1)*10;
            return menuManager.GetTopSellMenu(index);
        } 
    }
}
