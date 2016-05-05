using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Todoku.Models;
using System.Web.Security;
using System.Data;

namespace Todoku.Areas.Members.Controllers
{
    public class PurchasingController : Controller
    {
        //
        // GET: /Members/Purchasing/

        public ActionResult Index(Int32 Page = 1, Int32 Rows = SystemSetting.Default_Grid_Row)
        {
            BusinessLayer db = new BusinessLayer();
            Int32 UserID = db.GetUserProfileID();
            List<PurchaseReceiveHd> lst = null;
            if (UserID != 0)
            {
                lst = db.purchasereceivehds.Where(x => x.CustomerID == UserID && x.ReceiveStatus == ReceiveStatus.Open).ToList();
                if (lst.Count() % Rows == 0)
                {
                    ViewBag.Pages = lst.Count() / Rows;
                }
                else
                {
                    ViewBag.Pages = lst.Count() / Rows + 1;
                }

                ViewBag.Page = 0;
                if (lst.Count() > 0)
                {
                    ViewBag.Page = Page;
                    return View(lst.Skip((Page - 1) * Rows).Take(Rows).ToList());
                }
                else
                {
                    return View(lst);
                }
            }

            return View(lst);
        }

        public JsonResult Confirm(PurchaseReceiveHd entity)
        {
            try
            {
                BusinessLayer db = new BusinessLayer();
                PurchaseReceiveHd obj = db.purchasereceivehds.FirstOrDefault(x => x.ReceiveNo == entity.ReceiveNo);
                obj.PayerName = entity.PayerName;
                obj.SenderAccountNo = entity.SenderAccountNo;
                obj.TransferAmount = entity.TransferAmount;
                if (obj.TransferAmount < obj.TotalAmount)
                    obj.RefundAmount = obj.TransferAmount;
                else
                {
                    obj.RefundAmount = obj.TransferAmount - obj.TotalAmount;
                }
                obj.ReceiveStatus = ReceiveStatus.PayedByCustomer;
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { ok = true, message = "Success" });
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, message = ex.Message });
            }

        }
    }
}
