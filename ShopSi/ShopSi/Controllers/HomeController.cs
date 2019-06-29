using Models.Dao;
using Models.EF;
using ShopSi.Common;
using ShopSi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopSi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var dao = new ProductDao();
            ViewBag.NewProduct = dao.GetListNewProduct(6);
            ViewBag.FeatureProduct = dao.GetListFeatureProduct(3);
            return View();
        }
        [ChildActionOnly]
        public PartialViewResult TopMenu()
        {
            var model = new MenuDao().GetTopMenu(2);
            return PartialView(model);
        }

        [ChildActionOnly]
        public PartialViewResult ProductCategory()
        {
            var model = new ProductTypeDao().GetProductType();

            ViewBag.ProductCate1 = new ProductTypeDao().GetProductCategory(1);
            ViewBag.ProductCate2 = new ProductTypeDao().GetProductCategory(2);
            ViewBag.ProductCate3 = new ProductTypeDao().GetProductCategory(3);
            return PartialView(model);
        }

       
        public ActionResult ProductDetail(long id)
        {
            var model = new ProductDao().GetProductDetail(id);
            return View(model);
        }

        public ActionResult Category(long cateid,int page=1,int pageSize=6)
        {
            var model = new ProductCategoryDao().GetProductCategory(cateid);
            int totalRecode = 0;
            ViewBag.ProdcutCateId = new ProductCategoryDao().GetProductCateId(model.ID,ref totalRecode, page,pageSize);
            ViewBag.Total = totalRecode;
            ViewBag.Page = page;
            int maxPage = 5;
            int totalPage = 0;
            totalPage = (int)Math.Ceiling((double)totalRecode / pageSize);
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            ViewBag.FeatureProduct = new ProductDao().GetListFeatureProduct(6);
            ViewBag.ProType = new ProductTypeDao().GetProductType();
            return View(model);
        }

        [ChildActionOnly]
        public PartialViewResult Header()
        {

            var cart = Session[CommonConstant.cart_session];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
        
            return PartialView(list);
        }
    }
}