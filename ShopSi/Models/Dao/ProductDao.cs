using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EF;
using Common;

namespace Models.Dao
{
   public class ProductDao
    {
        ShopSiDbContext db = null;
        public ProductDao()
        {
            db = new ShopSiDbContext();
        }

        public IEnumerable<Product> GetSearching(string search)
        {
            IEnumerable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.Name.Contains(search) || x.Description.Contains(search));
            }
            return model.OrderByDescending(x => x.CreatedDate);
        }
        public List<Product> GetListNewProduct(int top)
        {
            var model = db.Products.OrderByDescending(x => x.CreatedDate).Take(top).ToList();
            return model;
        }

        public List<Product> GetListFeatureProduct(int top)
        {
            var model = db.Products.Where(x=>x.TopHot!=null&&x.TopHot>DateTime.Now).OrderByDescending(x => x.CreatedDate).Take(top).ToList();
            return model;
        }

        public Product GetProductDetail(long id)
        {
            var model = db.Products.Find(id);
            return model;
        }
        public long GetCreateProduct(Product model)
        {
            if (string.IsNullOrEmpty(model.MetaTitle))
            {
                model.MetaTitle = StringHelper.ToUnsignString(model.Name);

            }
            db.Products.Add(model);
            db.SaveChanges();
            return model.ID;
        }

        public List<string> GetListName (string keyword)
        {
            return db.Products.Where(x => x.Name.Contains(keyword)).Select(x => x.Name).ToList();
        }
    }
}
