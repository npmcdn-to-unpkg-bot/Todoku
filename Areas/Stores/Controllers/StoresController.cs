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

        public ActionResult Index(String category = "")
        {
            BusinessLayer db = new BusinessLayer();
            if (category == "")
                return View(db.products.ToList());
            else
                return View(db.products.Where(x => x.Category.Contains(category)).ToList());
        }
    }
}
