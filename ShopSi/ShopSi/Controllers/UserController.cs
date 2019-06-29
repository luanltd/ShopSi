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
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session[CommonConstant.user_session] = null;
            return Redirect("/");
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.UserName, MaHoaMD5.ToMD5(model.Password));
                if (result == 1)
                {

                    var sess = new CommonLogin();
                    var userID = dao.GetById(model.UserName);
                    sess.ID = userID;
                    sess.UserName = model.UserName;
                    Session[CommonConstant.user_session] = sess;

                    return Redirect("/");
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
            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            var dao = new UserDao();
            if (ModelState.IsValid)
            {
                if (dao.CheckUserName(model.UserName)==false)
                {
                    ModelState.AddModelError("", "Tài khoản đã tồn tại");
                }
                else if (dao.CheckEmail(model.Email)==false)
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                }
                else
                {
                    var user = new User();
                    user.UserName = model.UserName;
                    user.Password = Common.MaHoaMD5.ToMD5(model.Password);
                    user.Address = model.Address;
                    user.Phone = model.Phone;
                    user.Name = model.Name;
                    user.CreatedDate = DateTime.Now;
                    user.Status = model.Status;
                    var result=dao.Insert(user);
                    if (result > 0)
                    {
                        return Redirect("/");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng ký thất bại");
                    }
                }
               
            }
           
            return View(model);
        }
    }
}