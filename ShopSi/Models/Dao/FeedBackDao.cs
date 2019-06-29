using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EF;

namespace Models.Dao
{
   public class FeedBackDao
    {
        ShopSiDbContext db = null;
        public FeedBackDao()
        {
            db = new ShopSiDbContext();
        }
        public int  GetFeedBack(Feedback fb)
        {
            db.Feedbacks.Add(fb);
            db.SaveChanges();
            return fb.ID;
        }
    }
}
