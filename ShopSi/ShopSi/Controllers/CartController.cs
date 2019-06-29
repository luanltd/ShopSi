using Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopSi.Common;
using ShopSi.Models;
using System.Web.Script.Serialization;
using Models.EF;
using Common;

namespace ShopSi.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CommonConstant.cart_session];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        public ActionResult AddItem(long productID, int quantity)
        {
            var product = new ProductDao().GetProductDetail(productID);
            var cart = Session[CommonConstant.cart_session];
            
            if (cart != null)
            {
                var list=(List<CartItem>)cart;
                if (list.Exists(x => x.Product.ID == productID))
                {
                    foreach(var item in list)
                    {
                        if (item.Product.ID == productID)
                        {
                            item.Quantity++;
                        }
                    }
                }
                else
                {
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                Session[CommonConstant.cart_session] = list;
            }
            else
            {
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                var listCart = new List<CartItem>();
                listCart.Add(item);
                Session[CommonConstant.cart_session] = listCart;                
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult Update(string cartModel)
        {
            var cartJson = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var cartSession =(List<CartItem>)Session[CommonConstant.cart_session];
            foreach ( var item in cartSession)
            {
                var jsonItem = cartJson.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            return Json(new
            {
                status=true
            });
        }

       
        public JsonResult DeleteAll()
        {
            Session[CommonConstant.cart_session] = null;
            return Json(new
            {
                status = true
            });
        }


        public JsonResult Delete(long id)
        {
            var cartSession = (List<CartItem>)Session[CommonConstant.cart_session];
            cartSession.RemoveAll(x => x.Product.ID == id);
            Session[CommonConstant.cart_session] = cartSession;
            return Json(new
            {
                status = true
            });
        }
        [HttpGet]
        public ActionResult Payment()
        {

            var cart = Session[CommonConstant.cart_session];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }


        [HttpPost]
        public ActionResult Payment(string shipname, string phone, string address, string email)
        {
            var order = new Order();
            order.CreatedDate = DateTime.Now;
            order.ShipName = shipname;
            order.ShipMobie = phone;
            order.ShipEmail = email;
            order.ShipAddress = address;
            try
            {
                var id = new OrderDao().GetOrder(order);
                var cart = (List<CartItem>)Session[CommonConstant.cart_session];
                decimal total = 0;
                //var nameproduct = "";
                //var quantity = 0;
                foreach (var item in cart)
                {
                    var orderdetail = new OrderDetail();
                    orderdetail.ProductID = item.Product.ID;
                    orderdetail.OrderID = id;
                    orderdetail.Price = item.Product.Price;
                    orderdetail.Quantity = item.Quantity;
                    new OrderDao().GetOrderDetail(orderdetail);
                    total += item.Product.Price.GetValueOrDefault(0) * item.Quantity;
                    

                }

                string content = System.IO.File.ReadAllText(Server.MapPath("~/assets/client/template/neworder.html"));
                content = content.Replace("{{CustomerName}}",shipname);
                content = content.Replace("{{phone}}", phone);
                content = content.Replace("{{address}}", address);
                content = content.Replace("{{email}}", email);
                content = content.Replace("{{total}}", total.ToString());
                //foreach(var i in cart)
                //{
                //    nameproduct = i.Product.Name;
                //    quantity = i.Quantity;
                //    content = content.Replace("{{nameproduct}}", nameproduct);
                //    content = content.Replace("{{quantity}}", quantity.ToString());
                //}
                //content = content.Replace("{{nameproduct}}", nameproduct);
                //content = content.Replace("{{quantity}}", quantity.ToString());
                new MailHelper().SendMail(email, "Đơn hàng mới từ Shop Si", content);
             

            }
            catch(Exception ex)
            {
                return Redirect("/khong-hoan-thanh");
            }

            return Redirect("/hoan-thanh");
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}