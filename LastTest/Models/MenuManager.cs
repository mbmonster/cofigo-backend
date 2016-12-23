using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LastTest.Models
{
    public class MenuManager : IMenuManager
    {
        CoffeeServicesEntities db = new CoffeeServicesEntities();
        List<MenuStoreInfo> menus = new List<MenuStoreInfo>();
        public MenuManager()
        {
            var menu = (from st in db.Stores join mn in db.Menus on st.ID equals mn.IDStore 
            
                        select new
                               {
                                   mn.Name,
                                   mn.Price,
                                   mn.OfferPercent,
                                   mn.Selled,
                                   mn.Image,
                                   st.NameStore,
                                   st.Address,
                                   st.Latitude,
                                   st.Longtitude
                               }).ToList();
            foreach (var item in menu)
            {
                menus.Add(new MenuStoreInfo
                          {
                              
                              Name = item.Name,
                              Price = Convert.ToDouble(item.Price),
                              OfferPercent = Convert.ToInt32(item.OfferPercent),
                              Selled = Convert.ToInt32(item.Selled),
                              Image = item.Image,
                              NameStore = item.NameStore,
                              Address = item.Address,
                              Latitude = Convert.ToDouble(item.Latitude),
                              Longtitude = Convert.ToDouble(item.Longtitude)
                          });
            }
        }
        public List<MenuStoreInfo> GetTopOfferMenu(int index)
        {
            var offermenu = menus.OrderByDescending(p => p.OfferPercent).Skip(index).Take(6).ToList();
            return offermenu;
        }



        public List<MenuStoreInfo> GetTopSellMenu(int index)
        {
            var sellmenu = menus.OrderByDescending(p => p.Selled).Skip(index).Take(6).ToList();
            return sellmenu;
        }
    }
}