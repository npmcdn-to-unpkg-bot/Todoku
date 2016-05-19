using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Todoku.Models;
using System.Web.Security;
using System.Data;
using Newtonsoft.Json;

namespace Todoku.Areas.Members.Controllers
{
    public class OrderController : Controller
    {
        //
        // GET: /Members/Order/

        public ActionResult Index(Int32 Page = 1, Int32 Rows = SystemSetting.Default_Grid_Row)
        {
            BusinessLayer db = new BusinessLayer();
            Int32 UserID = db.GetUserProfileID();
            List<PurchaseOrderHd> lst = null;
            if (UserID != 0)
            {
                lst = db.purchaseorderhds.Where(x => x.CustomerID == UserID && x.OrderStatus == OrderStatus.Open).ToList();
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

        public ActionResult Process(int id = 0) 
        {
            BusinessLayer db = new BusinessLayer();
            Int32 UserID = db.GetUserProfileID();
            PurchaseOrderHd pohd = db.purchaseorderhds.FirstOrDefault(x => x.OrderID == id && x.CustomerID == UserID && x.OrderStatus == OrderStatus.Open);
            if (pohd != null)
            {
                List<StandardCode> lst = db.standardcodes.Where(x => x.ParentID == SCConstant.Cara_Pembayaran).ToList();
                ViewBag.Banks = db.banks.ToList();
                ViewBag.PaymentMethod = new SelectList(lst, "StandardCodeID", "StandardCodeName");
                ViewBag.TotalWeight = pohd.orderdetails.Sum(x => x.cart.product.detail.Weight);

                List<ProductAttribute> productAtt = db.productAttributes.Where(x => !x.IsDeleted).ToList();
                ViewBag.ProductAtt = productAtt;

                //pohd.Address = up.address.Address;
                return View(pohd);
            }
            else return RedirectToAction("index");
        }

        [HttpPost]
        public JsonResult Confirmation(PurchaseOrderConfirmation entity)
        {
            BusinessLayer db = new BusinessLayer();
            try
            {
                PurchaseOrderHd obj = db.purchaseorderhds.FirstOrDefault(x => x.OrderID == entity.OrderID || x.OrderNo == entity.OrderNo);
                obj.PaymentMehod = entity.PaymentMehod;
                if (entity.AgentID != 0) obj.AgentID = entity.AgentID;
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
                //return View(obj);
                return Json(new { ok = true, message = "Success" });
            }
            catch (Exception ex)
            {
                //return RedirectToAction("index", "PurchaseOrder", new { OrderNo = entity.OrderNo });
                return Json(new { ok = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Void(Int32 id) 
        {
            try
            {
                BusinessLayer db = new BusinessLayer();
                PurchaseOrderHd obj = db.purchaseorderhds.Find(id);
                obj.OrderStatus = OrderStatus.Void;
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { ok = true, Message = "Data berhasil dihapus"});
            }
            catch (Exception ex) 
            {
                return Json(new { ok = false, Message = "Data gagal dihapus : " + ex.Message });
            }
        }

        [HttpPost]
        public JsonResult VoidAll(List<Int32> list) 
        {
            JsonResult json = null;
            try
            {
                foreach (Int32 id in list) 
                {
                    json = Void(id);
                }
            }
            catch (Exception ex) 
            {
                return Json(new { ok = false, Message = "Data gagal dihapus : " + ex.Message });
            }
            return json;
        }

        public ActionResult ProcessAll(String list)
        {
            ViewBag.ListID = list;
            return View();
        }

        #region AJAX Request
        public JsonResult GetPurchaseOrderHdList(List<Int32> list = null)
        {
            BusinessLayer db = new BusinessLayer();
            Int32 UserID = db.GetUserProfileID();
            List<PurchaseOrderHd> lst = null;
            if (list == null)
                lst = db.purchaseorderhds.Where(x => x.CustomerID == UserID && x.OrderStatus == OrderStatus.Open).ToList();
            else
                lst = db.purchaseorderhds.Where(x => x.CustomerID == UserID && x.OrderStatus == OrderStatus.Open && list.Any(s => s == x.OrderID)).ToList();
            return new JsonNetResult(lst, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBankList()
        {
            BusinessLayer db = new BusinessLayer();
            List<Bank> lst = db.banks.Where(x => !x.IsDeleted).ToList();
            return new JsonNetResult(lst, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetShippingAddressList() 
        { 
            BusinessLayer db = new BusinessLayer();
            Int32 UserID = db.GetUserProfileID();
            List<ShippingAddresses> lst = db.ShippingAddresses.Where(x => !x.IsDeleted && x.UserProfileID == UserID).ToList();
            return new JsonNetResult(lst, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
