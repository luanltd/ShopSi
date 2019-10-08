﻿using Models.Dao;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Xml.Linq;
using ShopSi.Areas.Admin.Models;
using ShopSi.Common;

namespace ShopSi.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        //[HasCredential(RoleID="VIEW_USER")]
        public ActionResult Index(string searching, int page = 1,int pageSize=3)
        {

            var model = new UserDao().GetAllUser(searching,page,pageSize);
            ViewBag.search = searching;
            return View(model);
        }

        
        [HttpGet]
        //Thêm bản ghi, hiển thị form thêm bản
        //[HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        //Thêm bản ghi, nhận nội dung khi nhấn submit
        //[HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create(User model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (dao.CheckUserName(model.UserName) == false)
                {
                    ModelState.AddModelError("", "Tài khoản đã tồn tại");
                }
                else if (dao.CheckEmail(model.Email) == false)
                {
                    ModelState.AddModelError("", "Email đã tồn tại");

                }
                else
                {
                    model.CreatedDate = DateTime.Now;
                    if (!string.IsNullOrEmpty(model.Password))
                    {
                        var pwd = Common.MaHoaMD5.ToMD5(model.Password);
                        model.Password = pwd;
                    }
                    var result = dao.Insert(model);
                    if (result > 0)
                    {
                        ModelState.AddModelError("", "Thêm thành công");
                        return RedirectToAction("Index", "User");

                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm thất bại");
                    }
                }

            }

            return View();
        }

        [HttpGet]
        //Sửa bản ghi, hiển thị thông tin trên form
        //[HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(long id)
        {
            var model = new UserDao().GetUserById(id);
            return View(model);
        }
       
        [HttpPost]
        //Sửa bản ghi
        //[HasCredential(RoleID = "EDIT_USER")]
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
      
        //[HttpDelete]
        //public ActionResult Delete(long id)
        //{
        //    var dao = new UserDao().DeleteById(id);
        //    return RedirectToAction("Index","User");
        //}
        
        [HttpPost]
        //hàm xóa user trả về dạng json gọi từ ajax
        //[HasCredential(RoleID = "DELETE_USER")]
        public JsonResult Delete(long id)
        {
            new UserDao().DeleteById(id);
            return Json(new {
                status=true
            });
        }


        [HttpPost]
        //hàm load province trả về json cho javascript
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

        //hàm load district trả về json cho javascript
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
        
        [HttpPost]
        //hàm thay đổi trạng thái trả về json
        //[HasCredential(RoleID = "EDIT_USER")]
        public JsonResult ChangeStatus(long id)
        {
            var result = new UserDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}