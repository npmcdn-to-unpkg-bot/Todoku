using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Todoku.Models;

namespace Todoku.Areas.Merchants.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Merchants/Home/

        public ActionResult Index(Int32? DashboardID, Int32 MerchantID = 0)
        {
            TempData["DashboardID"] = DashboardID;
            TempData["MerchantID"] = MerchantID;
            if (MerchantID != 0)
            {
                TempData["MerchantID"] = MerchantID;
            }
            else
            {
                BusinessLayer db = new BusinessLayer();
                Int32 UserID = db.GetUserProfileID();
                Merchant merchant = db.merchants.FirstOrDefault(x => x.IsActive && !x.IsDeleted && x.OwnerID == UserID);
                if(merchant != null) TempData["MerchantID"] = merchant.MerchantID;
            }
            return View();
        }
    }
}
