using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LastTest.Models
{
    public class MenuManager : IMenuManager
    {
        CoffeeServicesEntities db = new CoffeeServicesEntities();
        List<Menu> menus = new List<Menu>();
        public MenuManager()
        {
            var menu = (from s in db.Menus select s).ToList();
            foreach (var item in menu)
            {
                menus.Add(new Menu
                          {
                              ID = item.ID,
                              Name = item.Name,
                              Price = item.Price,
                              OfferPercent = item.OfferPercent,
                              Selled = item.Selled,
                              IDStore = item.IDStore,
                              Image = item.Image
                          });
            }
        }
        public List<Menu> GetTopOfferMenu(int index)
        {
            var offermenu = menus.OrderByDescending(p => p.OfferPercent).Skip(index).Take(10).ToList();
            return offermenu;
        }



        public List<Menu> GetTopSellMenu(int index)
        {
            var sellmenu = menus.OrderByDescending(p => p.Selled).Skip(index).Take(10).ToList();
            return sellmenu;
        }
    }
}