using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace LastTest.Models
{
    public interface IStoreManager
    {
        List<Store> GetStores(int page);
        Store GetStore(int id);
        Store AddStore(Store store);
       // bool UpdateStore( Store store);
        void Delete(int id);
      string SearchList(string id);
        List<Store> GetTopStore();
    }
}
