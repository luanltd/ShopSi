using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EF;


namespace Models.Dao
{
   public class CategoryDao
    {
        ShopSiDbContext db = null;
        public CategoryDao()
        {
            db = new ShopSiDbContext();
        }
        public List<Category> ListAll()
        {
            return db.Categories.Where(x => x.Status == true).ToList();
        }
    }
}
