using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Todoku.Models;

namespace Todoku.Controllers
{
    public class PartialViewController : Controller
    {
        BusinessLayer db = new BusinessLayer();
        
        [ChildActionOnly]
        public ActionResult Header()
        {
            String UserName = "";
            if (User.Identity.IsAuthenticated)
            {
                UserName = Membership.GetUser().UserName;
                List<Cart> carts = db.carts.Where(x => x.UserName == UserName && x.ItemStatus == ItemStatus.Requested).ToList();
                ViewData["CartQty"] = carts.Sum(x => x.Quantity);
            }
            else 
            {
                List<Cart> carts = AppSession.GetCartUsingCookie(this.HttpContext);
                ViewData["CartQty"] = carts.Sum(x => x.Quantity);
            }
            
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Breadcrumbs()
        {
            //String url = Request.RawUrl;
            //String[] arr = url.Split('/');

            //List<Menu> menus = new List<Menu>();
            //Int32 count = 0;
            //if (!url.Contains("Home"))
            //{
            //    menus.Add(new Menu { MenuName = "Home", Path = "/" });
            //}
            
            //String path = "";
            //foreach (string obj in arr.Skip(2))
            //{
            //    int temp;
            //    if (int.TryParse(obj, out temp))
            //    {
            //        path += "/" + obj;
            //        menus[count].Path = path;

            //        if (menus.Any(x => x.MenuName.Contains("Product")))
            //        {
            //            Menu menu = menus.FirstOrDefault(x => x.MenuName.Contains("Product"));
            //            BusinessLayer db = new BusinessLayer();
            //            Product p = db.products.Find(temp);
            //            menu.Path += String.Format("?category={0}", p.Category);
            //        }
            //    }
            //    else
            //    {
            //        path = path + "/" + obj;
            //        menus.Add(new Menu { MenuName = obj.Split('?')[0], Path = path });
            //    }
            //    count++;
            //}
            //ViewBag.menus = menus;
            ViewBag.menus = "";
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult AddToCart(Int32 ProductID) 
        {
            Cart cart = new Cart();
            cart.ProductID = ProductID;
            cart.Quantity = 1;
            return PartialView(cart);
        }

        [ChildActionOnly]
        public ActionResult LeftPanel(String filterExpression = "") 
        {
            String[] setting = filterExpression.Split(';');
            foreach (String obj in setting) 
            {
                switch (obj) 
                {
                    case "Member":
                        ViewBag.MemberMenu = true;
                        break;
                    case "Merchant":
                        ViewBag.MerchantMenu = true;            
                        break;
                    case "Merchant_Detail":
                        ViewBag.MerchantDetail = true;
                        break;
                }
            }
            return PartialView();
        }
    }
}
