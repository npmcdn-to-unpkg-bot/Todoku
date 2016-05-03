using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Todoku.Areas.Admin.Controllers
{
    public class RegistrationController : Controller
    {
        //
        // GET: /Admin/Registration/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MerchantRegistration() 
        {
            return View();
        }

        public ActionResult AgentRegistration() 
        {
            return View();
        }
    }
}
