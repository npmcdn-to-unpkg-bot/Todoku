using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Todoku.Models;

namespace Todoku.Areas.Stores.Controllers
{
    public class StoresController : Controller
    {
        //
        // GET: /Stores/Home/

        public ActionResult Index(String category = "", String filterExpression = "")
        {
            BusinessLayer db = new BusinessLayer();
            ViewBag.Categories = db.standardcodes.Where(x => x.ParentID == SCConstant.Kategori_Produk).OrderBy(x => x.StandardCodeName).ToList();
            if (category == "")
                return View(db.products.Where(x => x.ProductName.Contains(filterExpression)).ToList());
            else
                return View(db.products.Where(x => x.Category.Contains(category) && x.ProductName.Contains(filterExpression)).ToList());
        }
    }
}
