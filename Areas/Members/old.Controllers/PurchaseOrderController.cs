using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity;
using Todoku.Models;
using System.Data;

namespace Todoku.Areas.Members.Controllers
{
    [Authorize]
    public class PurchaseOrderController : Controller
    {
        //
        // GET: /PurchaseOrder/
        
        public ActionResult Index(String OrderNo = "")
        {
            BusinessLayer db = new BusinessLayer();
            String UserName = Membership.GetUser().UserName;
            UserProfile up = db.userprofiles.FirstOrDefault(x => x.UserName == UserName);
            PurchaseOrderHd pohd = db.purchaseorderhds.FirstOrDefault(x => x.OrderNo == OrderNo && x.CustomerID == up.UserProfileID && x.OrderStatus == OrderStatus.Open);
            List<StandardCode> lst = db.standardcodes.Where(x => x.ParentID == SCConstant.Cara_Pembayaran).ToList();
            ViewBag.Banks = db.banks.ToList();
            ViewBag.PaymentMethod = new SelectList(lst,"StandardCodeID","StandardCodeName");
            pohd.Address = up.address.Address;
            return View(pohd);
        }

        public ActionResult Confirmation(PurchaseOrderConfirmation entity) 
        {
            BusinessLayer db = new BusinessLayer();
            try
            {
                PurchaseOrderHd obj = db.purchaseorderhds.FirstOrDefault(x => x.OrderNo == entity.OrderNo);
                obj.PaymentMehod = entity.PaymentMehod;
                obj.AgentID = entity.AgentID;
                obj.Address = entity.Address;
                obj.ShippingCharges = entity.ShippingCharges;
                obj.OrderStatus = OrderStatus.Order;
                db.Entry(obj).State = EntityState.Modified;

                PurchaseReceiveHd prhd = new PurchaseReceiveHd();
                String DateTransaction = String.Format("{0}{1}{2}", DateTime.Now.Year, DateTime.Now.Month.ToString("00"), DateTime.Now.Day.ToString("00"));
                Int32 count = db.purchasereceivehds.Where(x => x.ReceiveNo.Contains(DateTransaction)).Count() + 1;
                prhd.ReceiveNo = String.Format("{0}/{1}/{2}", TransactionNoPrefix.Purchase_Receive, DateTransaction, count.ToString("000000"));
                prhd.CustomerID = db.GetUserProfileID();
                prhd.ReceiveDate = DateTime.Now;
                prhd.Address = entity.Address;
                prhd.BankID = entity.BankID;
                prhd.ShippingCharges = entity.ShippingCharges;
                prhd.InsuranceCharges = 0;
                prhd.TotalAmount = obj.LineAmount;
                prhd.ReceiveStatus = ReceiveStatus.Open;
                prhd.CreatedBy = Membership.GetUser().UserName;
                prhd.CreatedDate = DateTime.Now;
                db.purchasereceivehds.Add(prhd);

                PurchaseReceiveDt prdt = new PurchaseReceiveDt();
                prdt.OrderID = obj.OrderID;
                prdt.receive = prhd;
                prdt.TotalAmount = obj.LineAmount;
                prdt.CreatedBy = Membership.GetUser().UserName;
                prdt.CreatedDate = DateTime.Now;
                db.purchasereceivedts.Add(prdt);

                db.SaveChanges();
                return View(obj);
            }
            catch (Exception ex) 
            {
                return RedirectToAction("index", "PurchaseOrder", new { OrderNo = entity.OrderNo });
            }
            
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

        public JsonResult ConfirmWithAjax(PurchaseReceiveHd entity)
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
