using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastTest.Models
{
    interface IMenuManager
    {
        List<Menu> GetTopOfferMenu(int index);
        List<Menu> GetTopSellMenu(int index);
    }
}
