using Models.Dao;
using Models.EF;
using ShopSi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopSi.Areas.Admin.Controllers
{
    public class ContentController : BaseController
    {
        // GET: Admin/Content
        public ActionResult Index(string searching, int page = 1, int pageSize = 3)
        {
            var model = new ContentDao().GetAllContent(searching, page, pageSize);
            ViewBag.search = searching;
            return View(model);
           
        }

        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();

        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var model = new ContentDao().GetById(id);
            SetViewBag(model.CategoryID);
            return View(model);
        }


        [HttpPost]
        public ActionResult Create(Content model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ViewCount = 0;
                var session =(CommonLogin)Session[CommonConstant.user_session];
                model.CreatedBy = session.UserName;
                new ContentDao().Create(model);
                return RedirectToAction("Index","Content");
            }
            SetViewBag();
            return View("Index");
        }

        [HttpPost]
        public ActionResult Edit(Content model)
        {
            if (ModelState.IsValid)
            {
               
                new ContentDao().Edit(model);
                return RedirectToAction("Index");
            }
            SetViewBag(model.CategoryID);
            return View();
        }

        public void SetViewBag(long? selectedId=null)
        {
            var dao = new CategoryDao();
            ViewBag.CategoryID=  new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }


    }
}