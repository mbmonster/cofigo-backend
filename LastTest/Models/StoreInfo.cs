using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LastTest.Models
{
    public class StoreInfo
    {
        public int ID { get; set; }
        
        public string NameStore { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
        public string Cover { get; set; }
        
        [Range(1,10)]
        public int Rep { get; set; }
        public double Latitude { get; set; }
        public double Longtitude { get; set; }
        public string Mobile { get; set; }
    }
}