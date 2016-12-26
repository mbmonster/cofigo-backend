using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LastTest.Models
{
    public class OrderInfo
    {
        public OrderInfo(string id, string idCustomer, int? idShip, DateTime? date, string status, string displayName, string SDT, double? Latitude, double? Longtitude,double? Total)
        {
            this.ID = id;
            this.IDCustomer = idCustomer;
            this.IDShip = Convert.ToInt32(idShip);
            this.Date = Convert.ToDateTime(date);
            this.Status = status;
            this.DisplayName = displayName;
            this.SDT = SDT;
           
            this.Latitude = Convert.ToDouble(Latitude);
            this.Longtitude = Convert.ToDouble(Longtitude);
            this.Total = Total;
        }

        public OrderInfo()
        {
            Items = new SelectList(new[] { "REQUEST", "CONFIRM", "SHIPPING", "DELIVERED" });
            CurrentItem = "REQUEST";
            // TODO: Complete member initialization
        }

        public string ID { get; set; }
        [DisplayName("Ngày Gởi")]
        public DateTime Date { get; set; }

        public string SDT { get; set; }
        
        public int IDShip { get; set; }
        [DisplayName("ID Khách Hàng")]
        public string IDCustomer { get; set; }
        [DisplayName("Tình Trạng")]
        public string Status { get; set; }
        [DisplayName("Tên Khách Hàng")]
        public string DisplayName { get; set; }
        public SelectList Items { get; set; }
        public string CurrentItem { get; set; }
        public double Latitude { get; set; }
        public double Longtitude { get; set; }
        public double? Total { get; set; }
    }
}