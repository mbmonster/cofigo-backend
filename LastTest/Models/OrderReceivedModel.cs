using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LastTest.Models
{
    public class OrderReceivedModel
    {
        public string ID { get; set; }
        public int IDShip { get; set; }
        public string arrayMenu { get; set; }
        public double Latitude { get; set; }
        public double Longtitude { get; set; }
        public string SDT { get; set; }
    }
}