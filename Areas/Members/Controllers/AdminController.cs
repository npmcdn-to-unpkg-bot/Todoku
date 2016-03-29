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
            ViewBag.PartialView = "LeftPanel";
            ViewBag.filterExpression = "Member";
            List<PurchaseOrderHd> pohds = db.purchaseorderhds.Where(x => x.OrderStatus == OrderStatus.Konfimasi).ToList();
            return View(pohds);
        }

        public JsonResult PaymentConfirm(PurchaseOrderHd entity) 
        {
            try
            {
                BusinessLayer db = new BusinessLayer();
                
                PurchaseOrderHd pohd = db.purchaseorderhds.FirstOrDefault(x => x.OrderID == entity.OrderID);
                if (pohd.TransferAmount >= (pohd.TotalAmount - pohd.RefundAmount) && entity.AgentID != null)
                {
                    pohd.AgentID = entity.AgentID;
                    pohd.OrderStatus = OrderStatus.Dibayar;
                    db.Entry(pohd).State = EntityState.Modified;

                    foreach (PurchaseOrderDt podt in pohd.orderdetails)
                    {
                        if (podt.OrderStatus != OrderStatus.Void)
                        {
                            podt.OrderStatus = OrderStatus.Dibayar;
                            podt.cart.ItemStatus = ItemStatus.Payed;
                        }
                        db.Entry(podt).State = EntityState.Modified;
                    }

                    List<CustomerOrder> cos = db.customerOrder.Where(x => x.OrderID == entity.OrderID).ToList();
                    foreach (CustomerOrder co in cos)
                    {
                        if (co.RequestStatus != RequestStatus.Void)
                            co.RequestStatus = RequestStatus.ConfirmedByAdmin;
                        db.Entry(co).State = EntityState.Modified;
                    }
                }
                else 
                {
                    pohd.OrderStatus = OrderStatus.Refund;
                    db.Entry(pohd).State = EntityState.Modified;

                    foreach (PurchaseOrderDt podt in pohd.orderdetails)
                    {
                        podt.OrderStatus = OrderStatus.Void;
                        db.Entry(podt).State = EntityState.Modified;
                    }

                    List<CustomerOrder> cos = db.customerOrder.Where(x => x.OrderID == entity.OrderID).ToList();
                    foreach (CustomerOrder co in cos)
                    {
                        co.RequestStatus = RequestStatus.Void;
                        db.Entry(co).State = EntityState.Modified;
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

        public JsonResult PaymentVoid(PurchaseOrderHd entity) 
        {
            try
            {
                BusinessLayer db = new BusinessLayer();
                PurchaseOrderHd obj = db.purchaseorderhds.Find(entity.OrderID);
                obj.OrderStatus = OrderStatus.Void;
                obj.LastUpdatedBy = Membership.GetUser().UserName;
                obj.LastUpdatedDate = DateTime.Now;
                db.Entry(obj).State = EntityState.Modified;

                foreach (PurchaseOrderDt podt in obj.orderdetails)
                {
                    podt.cart.ItemStatus = ItemStatus.Void;
                    podt.OrderStatus = OrderStatus.Void;
                    db.Entry(podt).State = EntityState.Modified;
                }

                List<CustomerOrder> cos = db.customerOrder.Where(x => x.OrderID == entity.OrderID).ToList();
                foreach (CustomerOrder co in cos)
                {
                    co.RequestStatus = RequestStatus.Void;
                    db.Entry(co).State = EntityState.Modified;
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
