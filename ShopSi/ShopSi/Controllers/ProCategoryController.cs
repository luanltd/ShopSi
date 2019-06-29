using Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopSi.Controllers
{
    public class ProCategoryController : Controller
    {
        // GET: ProCategory
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult LeftMain()
        {
            var model = new ProductTypeDao().GetProductType();

            ViewBag.ProductCate1 = new ProductTypeDao().GetProductCategory(1);
            ViewBag.ProductCate2 = new ProductTypeDao().GetProductCategory(2);
            ViewBag.ProductCate3 = new ProductTypeDao().GetProductCategory(3);
            return PartialView(model);
        }
    }
}