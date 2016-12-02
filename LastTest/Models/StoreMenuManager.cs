using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Newtonsoft.Json;


namespace LastTest.Models
{
    public class StoreMenuManager : IStoreMenuManager
    {
        CoffeeServicesEntities db = new CoffeeServicesEntities();
        
       
        


        public List<Menu> GetStoreMenus(int id)
        {
            List<Menu> listMenu = new List<Menu>();
            var list = (from mn in db.Menus
                        where mn.IDStore == id
                        select mn).ToList();
            foreach (var item in list)
            {
                listMenu.Add(new Menu
                {
                    ID = item.ID,
                    Name = item.Name,
                    Price = item.Price,
                    Selled = item.Selled,
                    OfferPercent = item.OfferPercent,
                    IDStore = item.IDStore
                });
            }
            return listMenu;
        }
        //public HttpResponseMessage


        public HttpResponseMessage SearchMenu(string id)
        {


            var dsa = db.Menus.Where(a => a.Name.Contains(id)).Select(a => new { a.ID, a.Name, a.Price, a.Selled, a.OfferPercent, a.IDStore, a.Store.NameStore, a.Store.Address }).ToList();
            //var dsa = (from mn in db.Menus
            // join st in db.Stores on mn.IDStore equals st.ID
            // select new ViewStoreMenu { MenuM = mn, StoreM = st }).ToList();
            var hrm = new HttpResponseMessage();
            hrm.Content = new StringContent(JsonConvert.SerializeObject(dsa));
            hrm.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
  
            return hrm;
        }


        
    }
}