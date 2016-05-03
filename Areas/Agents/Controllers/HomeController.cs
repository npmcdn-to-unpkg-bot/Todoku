using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Todoku.Areas.Agents.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Agents/Home/

        public ActionResult Index()
        {
            return View();
        }

    }
}
