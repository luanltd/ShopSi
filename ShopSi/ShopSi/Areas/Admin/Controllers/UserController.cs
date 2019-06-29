using Models.Dao;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Xml.Linq;
using ShopSi.Areas.Admin.Models;

namespace ShopSi.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(string searching, int page = 1,int pageSize=3)
        {

            var model = new UserDao().GetAllUser(searching,page,pageSize);
            ViewBag.search = searching;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var model = new UserDao().GetUserById(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(user.Password))
                {
                    var pwd = Common.MaHoaMD5.ToMD5(user.Password);
                    user.Password = pwd;
                }
                var model = new UserDao().Update(user);
                if (model)
                {
                    return RedirectToAction("Index","User");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhập thất bại");
                }
               
            }

            return View("Index");
        }

        [HttpPost]
        public ActionResult Create(User model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                var dao = new UserDao().Insert(model);
                if (dao > 0)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm thất bại");
                }
            }
            
            return View();
        }

        //[HttpDelete]
        //public ActionResult Delete(long id)
        //{
        //    var dao = new UserDao().DeleteById(id);
        //    return RedirectToAction("Index","User");
        //}


        [HttpPost]
        public JsonResult Delete(long id)
        {
            var dao = new UserDao().DeleteById(id);
            return Json(new {
                status=true
            });
        }
        [HttpPost]
        public JsonResult LoadProvince()
        {
            var xmlDoc = XDocument.Load(Server.MapPath(@"~/assets/admin/data/Provinces_Data.xml"));
            var xElements = xmlDoc.Element("Root").Elements("Item").Where(x=>x.Attribute("type").Value== "province");
            var list = new List<Province>();
           
            foreach(var item in xElements)
            {
                var province = new Province();
                province.ID = int.Parse(item.Attribute("id").Value);
                province.Name = item.Attribute("value").Value;
                list.Add(province);
            }
            return Json(new
            {
                data=list,
                status = true
            });
        }


        [HttpPost]
        public JsonResult LoadDistrict(int provinceID)
        {
            var xmlDoc = XDocument.Load(Server.MapPath(@"~/assets/admin/data/Provinces_Data.xml"));
            var xElements = xmlDoc.Element("Root").Elements("Item")
                .Single(x => x.Attribute("type").Value == "province" && int.Parse(x.Attribute("id").Value)==provinceID);
            var list = new List<District>();

            foreach (var item in xElements.Elements("Item").Where(x => x.Attribute("type").Value == "district"))
            {
                var district = new District();
                district.ID = int.Parse(item.Attribute("id").Value);
                district.Name = item.Attribute("value").Value;
                district.ProvinceID = int.Parse(xElements.Attribute("id").Value);
                list.Add(district);
            }
            return Json(new
            {
                data = list,
                status = true
            });
        }
    }
}