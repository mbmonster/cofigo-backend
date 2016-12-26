using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastTest.Models
{
    interface IPromotionManager
    {
        List<Promotion> GetTopPromotion();
    }
}
