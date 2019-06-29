using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EF;

namespace Models.Dao
{
    public class ProductTypeDao
    {
        ShopSiDbContext db = null;
        public ProductTypeDao()
        {
            db = new ShopSiDbContext();
        }
        public List<ProductType> GetProductType()
        {
            return db.ProductTypes.ToList();
        }

        public List<Product__Category> GetProductCategory(int typeID)
        {
            return db.Product__Categorys.Where(x => x.TypeID == typeID).ToList();
        }
    }
}
