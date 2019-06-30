using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EF;
using Models.ViewModel;
namespace Models.Dao
{
   public class ProductCategoryDao
    {
        ShopSiDbContext db = null;
        public ProductCategoryDao()
        {
            db = new ShopSiDbContext();
        }
        public Product__Category GetProductCategory(long cateid)
        {
            return db.Product__Categorys.SingleOrDefault(x => x.ID == cateid);
        }

        public List<Product> GetProductCateId(long cateid, ref int totalRecode, int page = 1, int pageSize = 6)
        {
            totalRecode = db.Products.Where(x => x.CategoryID == cateid).Count();
            return db.Products.Where(x => x.CategoryID == cateid).OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
