using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Todoku.Models;
using System.Web.Security;
using System.Data;

namespace Todoku.Areas.Admin.Controllers
{
    public class ConfirmationController : Controller
    {
        //
        // GET: /Admin/Purchase/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CustomerPayment(Int32 Page = 1, Int32 Rows = SystemSetting.Default_Grid_Row) 
        {
            BusinessLayer db = new BusinessLayer();
            List<PurchaseReceiveHd> prhd = db.purchasereceivehds.Where(x => x.ReceiveStatus == ReceiveStatus.PayedByCustomer).ToList();
            if (prhd.Count() % Rows == 0)
            {
                ViewBag.Pages = prhd.Count() / Rows;
            }
            else 
            {
                ViewBag.Pages = prhd.Count() / Rows + 1;
            }
            
            ViewBag.Page = 0;
            if (prhd.Count() > 0)
            {
                ViewBag.Page = Page;
                return View(prhd.Skip((Page - 1) * Rows).Take(Rows).ToList());
            }
            else 
            {
                return View(prhd);
            }
        }

        [HttpPost]
        public JsonResult CustomerPaymentConfirmed(PaymentConfirmation entity) 
        {
            try
            {
                BusinessLayer db = new BusinessLayer();
                PurchaseReceiveHd prhd = db.purchasereceivehds.FirstOrDefault(x => x.ReceiveID == entity.ReceiveID);
                prhd.ReceiveStatus = ReceiveStatus.ConfirmedByAdmin;
                
                if (prhd.TransferAmount >= prhd.TotalAmount)
                {
                    prhd.RefundAmount = prhd.TransferAmount - prhd.TotalAmount;
                    prhd.LastUpdatedBy = Membership.GetUser().UserName;
                    prhd.LastUpdatedDate = DateTime.Now;
                    db.Entry(prhd).State = EntityState.Modified;

                    foreach (PurchaseOrderHd obj in entity.lstAgentID) 
                    {
                        PurchaseOrderHd pohd = db.purchaseorderhds.FirstOrDefault(x => x.OrderID == obj.OrderID);
                        if (obj.AgentID != null)
                        {
                            pohd.AgentID = obj.AgentID;
                            db.Entry(pohd).State = EntityState.Modified;

                            List<CustomerOrder> cos = db.customerOrder.Where(x => x.OrderID == obj.OrderID).ToList();
                            foreach (CustomerOrder co in cos)
                            {
                                if (co.RequestStatus != RequestStatus.Void)
                                    co.RequestStatus = RequestStatus.ConfirmedByAdmin;
                                db.Entry(co).State = EntityState.Modified;
                            }
                        }
                        else 
                        {
                            return Json(new { ok = false, Status = "Harap masukkan no agen" });
                        }
                    }
                }
                else 
                {
                    prhd.RefundAmount = prhd.TotalAmount;
                    prhd.LastUpdatedBy = Membership.GetUser().UserName;
                    prhd.LastUpdatedDate = DateTime.Now;
                    db.Entry(prhd).State = EntityState.Modified;

                    foreach (PurchaseOrderHd obj in entity.lstAgentID)
                    {
                        PurchaseOrderHd pohd = db.purchaseorderhds.FirstOrDefault(x => x.OrderID == obj.OrderID);
                        pohd.OrderStatus = OrderStatus.Void;
                        db.Entry(pohd).State = EntityState.Modified;

                        List<CustomerOrder> cos = db.customerOrder.Where(x => x.OrderID == obj.OrderID).ToList();
                        foreach (CustomerOrder co in cos)
                        {
                            co.RequestStatus = RequestStatus.Void;
                            db.Entry(co).State = EntityState.Modified;
                        }
                    }
                }

                db.SaveChanges();
                return Json(new { ok = true, message = "Data berhasil disimpan" });
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CustomerPaymentVoid(PaymentConfirmation entity)
        {
            try
            {
                BusinessLayer db = new BusinessLayer();
                PurchaseReceiveHd prhd = db.purchasereceivehds.FirstOrDefault(x => x.ReceiveID == entity.ReceiveID);
                prhd.ReceiveStatus = ReceiveStatus.Void;
                prhd.LastUpdatedBy = Membership.GetUser().UserName;
                prhd.LastUpdatedDate = DateTime.Now;
                db.Entry(prhd).State = EntityState.Modified;

                foreach (PurchaseOrderHd obj in entity.lstAgentID)
                {
                    PurchaseOrderHd pohd = db.purchaseorderhds.FirstOrDefault(x => x.OrderID == obj.OrderID);
                    pohd.OrderStatus = OrderStatus.Void;
                    db.Entry(pohd).State = EntityState.Modified;

                    List<CustomerOrder> cos = db.customerOrder.Where(x => x.OrderID == obj.OrderID).ToList();
                    foreach (CustomerOrder co in cos)
                    {
                        co.RequestStatus = RequestStatus.Void;
                        db.Entry(co).State = EntityState.Modified;
                    }
                }

                db.SaveChanges();
                return Json(new { ok = true, Status = "Success" });
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, Status = ex.Message });
            }
        }

        public ActionResult MerchantDataChanged() 
        {
            return View();
        }
    }
}
