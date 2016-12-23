using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LastTest.Models
{
    public class MenuStoreInfo
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int OfferPercent { get; set; }
        public int Selled { get; set; }
        public string Image { get; set; }
        public string NameStore { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longtitude { get; set; }
    }
}