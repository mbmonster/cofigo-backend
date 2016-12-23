using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LastTest.Models;
using LastTest.COR;
namespace LastTest.Controllers
{
    [AllowCrossSiteJson]
    public class MenuController : ApiController
    {
        IMenuManager menuManager = new MenuManager();
        [HttpGet]
        public List<MenuStoreInfo> GetTopOffer([FromUri] string page)
        {
            var index = (Convert.ToInt32(page) - 1)*12;
            return menuManager.GetTopOfferMenu(index);
        }

        [HttpGet]
        public List<MenuStoreInfo> GetTopSelled([FromUri] string page)
        {
            var index = (Convert.ToInt32(page) - 1)*12;
            return menuManager.GetTopSellMenu(index);
        } 
    }
}
