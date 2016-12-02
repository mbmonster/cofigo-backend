﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LastTest.Models;

namespace LastTest.Controllers
{
    public class StoreMenuController : ApiController
    {
        
       IStoreMenuManager iStoreMenuManager = new StoreMenuManager();

        //public List<ViewStoreMenu> Get(int id)
        //{
        //    //var list = (from st in db.Stores
        //    //            join mn in db.Menus on st.ID equals mn.IDStore
        //    //            select new ViewStoreMenu { StoreM = st, MenuM = mn }).ToList();
            
        //}
        [HttpGet]
        public List<Menu> GetStoreMenus(int id)
        {
            return iStoreMenuManager.GetStoreMenus(id);
        }
        [HttpGet]
        public HttpResponseMessage SearchMenu(string id)
        {
            return iStoreMenuManager.SearchMenu(id);
        }
    }
}
