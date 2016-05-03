using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Todoku.Models;
using System.Web.Security;
using System.Data;

namespace Todoku.Areas.Merchants.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //
        // GET: /Merchants/Merchants/

        public ActionResult Index()
        {
            BusinessLayer db = new BusinessLayer();
            String Username = Membership.GetUser().UserName;
            List<Merchant> merchants = db.merchants.Where(x => x.userprofile.UserName == Username && x.IsActive == true).ToList();
            return View(merchants);
        }

        public ActionResult SendPackage() 
        {
            //BusinessLayer db = new BusinessLayer();
            //String OwnerID = Membership.GetUser().UserName;
            //List<Merchant> merchants = db.merchants.Where(x => x.IsActive && x.userprofile.UserName == OwnerID).ToList();
            //List<ItemDeliveryHd> itemdeliveryhds = new List<ItemDeliveryHd>();
            //foreach(Merchant entity in merchants)
            //{
            //    itemdeliveryhds.AddRange(db.itemdeliveryhds.Where(x => x.MerchantID == entity.MerchantID && x.DeliveryStatus == DeliveryStatus.Prepared).ToList());
            //}
            //return View(itemdeliveryhds);
            return View();
        }

        public JsonResult ProcessPackage(ItemDeliveryHd entity) 
        {
            try
            {
                BusinessLayer db = new BusinessLayer();
                ItemDeliveryHd itemDeliv = db.itemdeliveryhds.FirstOrDefault(x => x.DeliveryID == entity.DeliveryID);
                if (itemDeliv != null) 
                {
                    itemDeliv.DeliveryStatus = DeliveryStatus.Delivery;
                    itemDeliv.ReceiptNumber = entity.ReceiptNumber;
                    itemDeliv.LastUpdatedBy = Membership.GetUser().UserName;
                    itemDeliv.LastUpdatedDate = DateTime.Now;
                }
                db.Entry(itemDeliv).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { ok = true, Status = "Success" });
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, Status = ex.Message });
            }
            
        }
        
        public ActionResult Detail(Int32 id) 
        {
            BusinessLayer db = new BusinessLayer();
            Int32 userid = db.GetUserProfileID();
            Merchant merchant = db.merchants.FirstOrDefault(x => x.MerchantID == id && x.OwnerID == userid);
            return View(merchant);
        }
    }
}
