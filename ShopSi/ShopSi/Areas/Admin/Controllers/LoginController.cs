using Models.Dao;
using Models.EF;
using ShopSi.Areas.Admin.Models;
using ShopSi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopSi.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.UserName,MaHoaMD5.ToMD5(model.Password));
                if (result == 1)
                {
                    
                    var sess = new CommonLogin();
                    var userID = dao.GetById(model.UserName);
                    sess.ID = userID;
                    sess.UserName = model.UserName;
                    Session[CommonConstant.user_session] = sess;

                    return Redirect("/Admin/Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Bạn nhập sai mật khẩu");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                }
                else
                {
                    ModelState.AddModelError("", "Tài khoản đã bị khóa");
                }
            }
           

            return View("Index");
        }
    }
}