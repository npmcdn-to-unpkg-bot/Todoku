using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Todoku.Areas.Members.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Members/Home/

        [Authorize]
        public ActionResult Index(Int32 DashboardID)
        {
            TempData["DashboardID"] = DashboardID;
            return View();
        }

    }
}
