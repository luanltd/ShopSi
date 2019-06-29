using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.EF;
using Models.Dao;
using Common;

namespace ShopSi.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult FeedBack()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FeedBack(string name, string phone, string address, string email, string content)
        {
            var feedback = new Feedback();
            feedback.CreatedDate = DateTime.Now;
            feedback.Name = name;
            feedback.Phone = phone;
            feedback.Address = address;
            feedback.Email = email;
            feedback.Content = content;
            var result = new FeedBackDao().GetFeedBack(feedback);
            if (result > 0)
            {
                string fb = System.IO.File.ReadAllText(Server.MapPath("~/assets/client/template/feedback.html"));
                fb = fb.Replace("{{customername}}", name);
                fb = fb.Replace("{{phone}}", phone);
                fb = fb.Replace("{{email}}", email);
                fb = fb.Replace("{{address}}", address);
                fb = fb.Replace("{{content}}", content);
                new MailHelper().SendMail(email, "Nội dung phản hồi", fb);
                //new MailHelper().SendMail(email, "Đơn hàng mới từ Shop Si", content);
            }
            return Redirect("/hoan-thanh");
        }
    }
}