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
                var result = dao.Login(model.UserName,MaHoaMD5.ToMD5(model.Password),true);
                if (result == 1)
                {
                    
                    var sess = new CommonLogin();
                    var user = dao.GetUser(model.UserName);
                    
                    sess.ID = user.ID;
                    sess.UserName = user.UserName;
                    sess.GroupID = user.GroupID;
                    var credentials = dao.GetListCredential(model.UserName);
                    Session[CommonConstant.session_credential] = credentials;
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
                else if (result == 3)
                {
                    ModelState.AddModelError("", "Bạn không có quyền đăng nhập");
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