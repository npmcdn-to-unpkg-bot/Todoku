using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Todoku.Models;
using System.Data;
using System.Web.Security;

namespace Todoku.Areas.Members.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Members/Admin/

        public ActionResult Index()
        {
            BusinessLayer db = new BusinessLayer();
            List<PurchaseReceiveHd> pohds = db.purchasereceivehds.Where(x => x.ReceiveStatus == ReceiveStatus.PayedByCustomer).ToList();
            return View(pohds);
        }

        public JsonResult PaymentConfirm(PaymentConfirmation entity) 
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
                            pohd.OrderStatus = OrderStatus.Dibayar;
                            db.Entry(pohd).State = EntityState.Modified;
                        }

                        List<CustomerOrder> cos = db.customerOrder.Where(x => x.OrderID == obj.OrderID).ToList();
                        foreach (CustomerOrder co in cos)
                        {
                            if (co.RequestStatus != RequestStatus.Void)
                                co.RequestStatus = RequestStatus.ConfirmedByAdmin;
                            db.Entry(co).State = EntityState.Modified;
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
                return Json(new { ok = true, message = "Success" });
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, message = ex.Message });
            }
        }

        public JsonResult PaymentVoid(PaymentConfirmation entity) 
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
    }
}
