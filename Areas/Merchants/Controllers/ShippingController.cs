using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Todoku.Models;
using System.Data;

namespace Todoku.Areas.Merchants.Controllers
{
    public class ShippingController : Controller
    {
        //
        // GET: /Merchants/Shipping/

        public ActionResult Index()
        {
            BusinessLayer db = new BusinessLayer();
            String OwnerID = Membership.GetUser().UserName;
            Int32 MerchantID = Convert.ToInt32(TempData.Peek("MerchantID"));
            List<ItemDeliveryHd> itemdeliveryhds = db.itemdeliveryhds.Where(x => x.MerchantID == MerchantID && x.DeliveryStatus == DeliveryStatus.Prepared).ToList();
            return View(itemdeliveryhds);
        }

        public JsonResult Process(ItemDeliveryHd entity) 
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
                return Json(new { ok = true, message = "Data telah berhasil diproses" });
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, message = "Data tidak dapat diproses : " + ex.Message });
            }
        }
    }
}
