using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EF;

namespace Models.Dao
{
    public class OrderDao
    {
        ShopSiDbContext db = null;
        public OrderDao()
        {
            db = new ShopSiDbContext();
        }
        public long GetOrder(Order model)
        {
            db.Orders.Add(model);
            db.SaveChanges();
            return model.ID;
        }

        public bool GetOrderDetail(OrderDetail detail)
        {
            try
            {
                db.OrderDetails.Add(detail);
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
           

        }
    }
}
