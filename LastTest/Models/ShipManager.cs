using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LastTest.Models
{
    public class ShipManager:IShipManager
    {
        public List<Ship> getShips(){
            CoffeeServicesEntities db = new CoffeeServicesEntities();
            return db.Ships.ToList();
        }
    }
}