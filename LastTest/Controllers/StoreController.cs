﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using LastTest.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;


namespace LastTest.Controllers
{

    public class StoreController : ApiController
    {
      
        private IStoreManager storeManager = new StoreManager();
        [HttpGet]
        public IEnumerable<Store> Get()
        {
            return storeManager.GetStores();
        }
        [HttpGet]
        public Store Get(int id)
        {
            return storeManager.GetStore(id);
        }
        [HttpPost]
        public Store Add(Store store)
        {
           return storeManager.AddStore(store);
        }

        //public void Update(Store store)
        //{
        //    storeManager.UpdateStore(store);
        //}
        public void Delete(int id)
        {
            Store store = storeManager.GetStore(id);
            if (store == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            storeManager.Delete(id);
        }

        [HttpGet]
        
        public string SearchList(string name)
        {
            return storeManager.SearchList(name);
        } 
         
    }
}
