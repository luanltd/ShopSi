﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EF;

namespace Models.Dao
{
   public class ProductDao
    {
        ShopSiDbContext db = null;
        public ProductDao()
        {
            db = new ShopSiDbContext();
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
            db.Products.Add(model);
            db.SaveChanges();
            return model.ID;
        }
    }
}