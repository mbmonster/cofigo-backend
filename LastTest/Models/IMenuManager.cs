﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastTest.Models
{
    interface IMenuManager
    {
        List<MenuStoreInfo> GetTopOfferMenu(int index);
        List<MenuStoreInfo> GetTopSellMenu(int index);
    }
}
