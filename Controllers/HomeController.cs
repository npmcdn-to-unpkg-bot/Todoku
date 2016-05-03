using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Todoku.Models;

namespace Todoku.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(TempData.Peek("DefaultSystemName") == null)TempData["DefaultSystemName"] = SystemSetting.Default_System_Name;
            BusinessLayer db = new BusinessLayer();
            ViewBag.Categories = db.standardcodes.Where(x => x.ParentID == SCConstant.Kategori_Produk).OrderBy(x => x.StandardCodeName).ToList();
            return View(db.products.Take(10).ToList());
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
