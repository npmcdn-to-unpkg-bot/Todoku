using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Todoku.Models;

namespace Todoku.Areas.Merchants.Controllers
{
    public class PartialViewController : Controller
    {
        //
        // GET: /Merchants/PartialView/
        [ChildActionOnly]
        public ActionResult ListMerchant()
        {
            BusinessLayer db = new BusinessLayer();
            Int32 UserID = db.GetUserProfileID();
            List<Merchant> merchants = db.merchants.Where(x => x.IsActive && !x.IsDeleted && x.OwnerID == UserID).ToList();
            return PartialView(merchants);
        }

        [ChildActionOnly]
        public ActionResult ChangeMerchantName() 
        {
            BusinessLayer db = new BusinessLayer();
            String MerchantName = "Todoku";
            if (TempData.Peek("MerchantID") != null) 
            {
                Int32 MerchantId = Convert.ToInt32(TempData.Peek("MerchantID"));
                Merchant merchant = db.merchants.Find(MerchantId);
                if (merchant != null) MerchantName = merchant.MerchantName;
            }
            return PartialView((object)MerchantName);
        }
    }
}
