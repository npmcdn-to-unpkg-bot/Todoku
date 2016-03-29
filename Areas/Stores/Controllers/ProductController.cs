using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Todoku.Models;

namespace Todoku.Areas.Stores.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Stores/Product/
        
        //public ActionResult Index(String category = "")
        //{
        //    BusinessLayer db = new BusinessLayer();
        //    return View(db.products.Where(x => x.Category.Contains(category)).ToList());
        //}

        public ActionResult Detail(Int32 id)
        {
            BusinessLayer db = new BusinessLayer();
            Product product = db.products.Find(id);
            //ViewBag.menus = menus;
            return View(product);
        }

    }
}
