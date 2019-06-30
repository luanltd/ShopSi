using Facebook;
using Models.Dao;
using Models.EF;
using ShopSi.Common;
using ShopSi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopSi.Controllers
{
    public class UserController : Controller
    {
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult LoginFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = new FacebookClient().GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email"

            });
            return Redirect(loginUrl.AbsoluteUri);
        }
    


    public ActionResult FacebookCallback(string code)
    {
        var fb = new FacebookClient();
        dynamic result = fb.Post("oauth/access_token", new
        {
            client_id = ConfigurationManager.AppSettings["FbAppId"],
            client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
            redirect_uri = RedirectUri.AbsoluteUri,
            code = code
        });



        var accessToken = result.access_token;
        if (!string.IsNullOrEmpty(accessToken))
        {
            fb.AccessToken = accessToken;
            // Get the user's information, like email, first name, middle name etc
            dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
            string email = me.email;
            string userName = me.email;
            string firstname = me.first_name;
            string middlename = me.middle_name;
            string lastname = me.last_name;

            var user = new User();
            user.Email = email;
            user.UserName = email;
            user.Status = true;
            user.Name = firstname + " " + middlename + " " + lastname;
            user.CreatedDate = DateTime.Now;
            var resultInsert = new UserDao().InsertForFacebook(user);
            if (resultInsert > 0)
            {
                var userSession = new CommonLogin();
                userSession.UserName = user.UserName;
                userSession.ID = user.ID;
                Session.Add(CommonConstant.user_session, userSession);
            }
        }
        return Redirect("/");
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