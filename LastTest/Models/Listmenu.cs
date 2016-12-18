using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LastTest.Models
{
    public class Listmenu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
        public int? OfferPercent { get; set; }
        public int? Selled { get; set; }
        public int? IdStore { get; set; }
        public string NameStore { get; set; }
        public string Image { get; set; }

        public Listmenu(int id, string name, double? price, int? offerPercent, int? selled, int? idStore, string nameStore, string image)
        {
            Id = id;
            Name = name;
            Price = price;
            OfferPercent = offerPercent;
            Selled = selled;
            IdStore = idStore;
            NameStore = nameStore;
            Image = image;
        }

        public Listmenu()
        {
        }
    }
}