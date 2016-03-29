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

        public ActionResult Confirmation(PurchaseOrderHd entity) 
        {
            BusinessLayer db = new BusinessLayer();
            PurchaseOrderHd obj = db.purchaseorderhds.FirstOrDefault(x => x.OrderNo == entity.OrderNo);
            obj.PaymentMehod = entity.PaymentMehod;
            obj.AgentID = entity.AgentID;
            obj.Address = entity.Address;
            obj.BankID = entity.BankID;
            obj.OrderStatus = OrderStatus.Order;
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();
            return View(obj);
        }

        public JsonResult Confirm(PurchaseOrderHd entity)
        {
            try
            {
                BusinessLayer db = new BusinessLayer();
                PurchaseOrderHd obj = db.purchaseorderhds.FirstOrDefault(x => x.OrderNo == entity.OrderNo);
                obj.PayerName = entity.PayerName;
                obj.SenderAccountNo = entity.SenderAccountNo;
                obj.TransferAmount = entity.TransferAmount;
                if (obj.TransferAmount < obj.TotalAmount)
                    obj.RefundAmount = obj.TransferAmount;
                obj.OrderStatus = OrderStatus.Konfimasi;
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { ok = true, message = "Success" });
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, message = ex.Message });
            }
        }

        public JsonResult ConfirmWithAjax(PurchaseOrderHd entity)
        {
            try
            {
                BusinessLayer db = new BusinessLayer();
                PurchaseOrderHd obj = db.purchaseorderhds.FirstOrDefault(x => x.OrderNo == entity.OrderNo);
                obj.PayerName = entity.PayerName;
                obj.SenderAccountNo = entity.SenderAccountNo;
                obj.TransferAmount = entity.TransferAmount;
                if (obj.TransferAmount < obj.TotalAmount)
                    obj.RefundAmount = obj.TransferAmount;
                obj.OrderStatus = OrderStatus.Konfimasi;
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
