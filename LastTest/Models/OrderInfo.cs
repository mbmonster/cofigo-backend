using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LastTest.Models
{
    public class OrderInfo
    {
        public OrderInfo(string id, string idCustomer, int? idShip, DateTime? date, string status, string displayName)
        {
            this.ID = id;
            this.IDCustomer = idCustomer;
            this.IDShip = Convert.ToInt32(idShip);
            this.Date = Convert.ToDateTime(date);
            this.Status = status;
            this.DisplayName = displayName;
        }

        public string ID { get; set; }
        public DateTime Date { get; set; }
        public int IDShip { get; set; }
        public string IDCustomer { get; set; }
        public string Status { get; set; }
        public string DisplayName { get; set; }
    }
}