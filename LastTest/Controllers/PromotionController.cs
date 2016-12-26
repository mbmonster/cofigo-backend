using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using LastTest.Models;
using LastTest.COR;

namespace LastTest.Controllers
{
    [AllowCrossSiteJson]
    public class PromotionController : ApiController
    {
        IPromotionManager promManeger = new PromotionManager();

        [System.Web.Http.HttpGet]
        public List<Promotion> GetTopPromotion()
        {
            return promManeger.GetTopPromotion();
        }
    }
}
