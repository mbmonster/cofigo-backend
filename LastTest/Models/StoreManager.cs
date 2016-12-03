using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.BuilderProperties;
using Newtonsoft.Json;

namespace LastTest.Models
{
    public class StoreManager : IStoreManager
    {
        private CoffeeServicesEntities db = new CoffeeServicesEntities();
        private List<Store> stores = new List<Store>();

        public StoreManager()
        {
            var store = (from s in db.Stores select s);
            foreach (var s in store)
            {
                stores.Add(new Store
                           {
                               ID = s.ID,
                               NameStore = s.NameStore,
                               Address = s.Address,
                               Avatar = s.Avatar,
                               Cover = s.Cover,
                               Rep = s.Rep,
                               Latitude = s.Latitude,
                               Longtitude = s.Longtitude
                           });

            }
        }

        public List<Store> GetStores(int index)
        {

            return stores.Skip(index).Take(2).ToList();
        }

        public Store GetStore(int id)
        {
            return stores.FirstOrDefault(x => x.ID == id);
        }



        public Store AddStore(Store store)
        {
            if (store == null)
            {
                throw new ArgumentNullException("store");
            }

            db.Stores.Add(store);
            db.SaveChanges();
            return store;
        }
        //public bool UpdateStore(Store store)
        //{
        //    if (store == null)
        //    {
        //        throw new ArgumentNullException();
        //    }

        //    int index = stores.FindIndex(p => p.ID == store.ID);
        //    if (index == -1)
        //    {
        //        return false;
        //    }
        //    stores.RemoveAt(index);
        //    db.Stores.Add(store);
        //    db.SaveChanges();
        //    return true;
        //}
        public void Delete(int id)
        {
            stores.RemoveAll(p => p.ID == id);
        }
        public string SearchList(string id)
        {
            var store= db.Stores.Where(a => a.NameStore.Contains(id)).Select(a => new{a.ID, a.NameStore, a.Address, a.Avatar, a.Cover, a.Rep, a.Latitude, a.Longtitude}).ToList();
            return JsonConvert.SerializeObject(store);
        }



        public List<Store> GetTopStore()
        {
            var top = stores.OrderByDescending(p => p.Rep).Take(5);
            return top.ToList();
        }
    }

}