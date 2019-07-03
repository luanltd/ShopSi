using Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopSi.Controllers
{
    public class ContentController : Controller
    {
        // GET: Content
        public ActionResult Index(int page = 1,int pageSize=4)
        {
            int totalRecode = 0;
            var model = new ContentDao().GetAllContent(ref totalRecode,page, pageSize);                     
         
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
            return View(model);
        }

        public ActionResult Detail(long id)
        {
            var model = new ContentDao().GetById(id);
            ViewBag.Tags = new ContentDao().ListTag(id);
            return View(model);
        }

        public ActionResult Tag(string tagid,int page = 1, int pageSize = 10)
        {
            int totalRecode = 0;
            var model = new ContentDao().ListAllByTag(ref totalRecode, tagid,page, pageSize);           
            ViewBag.Total = totalRecode;
            ViewBag.Page = page;
            ViewBag.Tag = new ContentDao().GetTag(tagid);
            int maxPage = 5;
            int totalPage = 0;
            totalPage = (int)Math.Ceiling((double)totalRecode / pageSize);
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            return View(model);
        }
    }
}