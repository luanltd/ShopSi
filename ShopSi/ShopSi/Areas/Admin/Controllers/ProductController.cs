using Models.Dao;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopSi.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(Product model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                var result = new ProductDao().GetCreateProduct(model);
                if (result > 0)
                {
                    return RedirectToAction("SuccessAdmin","Product");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm thất bại");
                }
            }
           
            return View("Index");
        }
        public ActionResult SuccessAdmin()
        {
            return View();
        }
    }
}