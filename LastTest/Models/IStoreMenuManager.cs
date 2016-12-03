using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LastTest.Models
{
    public interface IStoreMenuManager
    {
        List<Menu> GetStoreMenus(int id);
        HttpResponseMessage SearchMenu(string id, int index);

    }
}
