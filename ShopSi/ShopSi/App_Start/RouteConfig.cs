using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShopSi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "Dangnhap",
               url: "dang-nhap",
               defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional },
               namespaces: new string[] { "ShopSi.Controllers" }

           );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "Dangky",
               url: "dang-ki",
               defaults: new { controller = "User", action = "Register", id = UrlParameter.Optional },
               namespaces: new string[] { "ShopSi.Controllers" }

           );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "Contact",
               url: "contact",
               defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
               namespaces: new string[] { "ShopSi.Controllers" }

           );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "Detail",
               url: "chi-tiet/{metatitle}-{id}",
               defaults: new { controller = "Home", action = "ProductDetail", id = UrlParameter.Optional },
               namespaces: new string[] { "ShopSi.Controllers" }

           );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "Category",
               url: "san-pham/{product}/{metatitle}-{cateid}",
               defaults: new { controller = "Home", action = "Category", id = UrlParameter.Optional },
               namespaces: new string[] { "ShopSi.Controllers" }

           );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "AddItem",
               url: "them-gio-hang",
               defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional },
               namespaces: new string[] { "ShopSi.Controllers" }

           );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "Cart",
               url: "gio-hang",
               defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
               namespaces: new string[] { "ShopSi.Controllers" }

           );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "Payment",
               url: "thanh-toan",
               defaults: new { controller = "Cart", action = "Payment", id = UrlParameter.Optional },
               namespaces: new string[] { "ShopSi.Controllers" }

           );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "Success",
               url: "hoan-thanh",
               defaults: new { controller = "Cart", action = "Success", id = UrlParameter.Optional },
               namespaces: new string[] { "ShopSi.Controllers" }

           );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "News",
               url: "tin-tuc",
               defaults: new { controller = "Content", action = "Index", id = UrlParameter.Optional },
               namespaces: new string[] { "ShopSi.Controllers" }

           );


            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "News Detail",
               url: "tin-tuc/{metatitle}-{id}",
               defaults: new { controller = "Content", action = "Detail", id = UrlParameter.Optional },
               namespaces: new string[] { "ShopSi.Controllers" }

           );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "Tags",
               url: "tag/{tagid}",
               defaults: new { controller = "Content", action = "Tag", id = UrlParameter.Optional },
               namespaces: new string[] { "ShopSi.Controllers" }

           );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "Search",
               url: "tim-kiem",
               defaults: new { controller = "Home", action = "Search", id = UrlParameter.Optional },
               namespaces: new string[] { "ShopSi.Controllers" }

           );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "Thuong hieu",
               url: "thuong-hieu",
               defaults: new { controller = "Content", action = "Thuonghieu", id = UrlParameter.Optional },
               namespaces: new string[] { "ShopSi.Controllers" }

           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] {"ShopSi.Controllers"}

            );
        }
    }
}
