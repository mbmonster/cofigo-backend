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
    public class ShipController : ApiController
    {
        private IShipManager shipManager = new ShipManager();
        [HttpGet]
        public List<Ship> Get()
        {
            CoffeeServicesEntities db = new CoffeeServicesEntities();
            var listOfShips = new List<Ship>();

            foreach (var ship in db.Ships)
            {

                listOfShips.Add(new Ship
                {
                    ID = ship.ID,
                    Price = ship.Price,
                    Description = ship.Description,
                    Max = ship.Max,
                    Min = ship.Min,
                    OfferPercent = ship.OfferPercent
                });
            }
            return listOfShips;
        }
    }
}
