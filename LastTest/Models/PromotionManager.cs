using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LastTest.Models
{
    public class PromotionManager:IPromotionManager
    {
        CoffeeServicesEntities db = new CoffeeServicesEntities();
        List<Promotion> promotions = new List<Promotion>();

        public PromotionManager()
        {
            var promotion = (from s in db.Promotions select s).ToList();
            foreach (var item in promotion)
            {
                promotions.Add(new Promotion
                {
                    ID = item.ID,
                    Title = item.Title,
                    Description = item.Description,
                    Image = item.Image,
                    IDStore = item.IDStore,
                    Created = item.Created,
                    Last = item.Last,
                    IDMenu = item.IDMenu
                });
            }
        }
        public List<Promotion> GetTopPromotion()
        {
            var pro = promotions.OrderByDescending(p => p.Created).Take(6).ToList();
            return pro;
        }
    }
}