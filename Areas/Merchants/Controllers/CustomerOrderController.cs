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
    public class CustomerOrderController : Controller
    {
        //
        // GET: /Merchants/CustomerOrder/

        public ActionResult Index(Int32 MerchantID)
        {
            BusinessLayer db = new BusinessLayer();
            String OwnerID = Membership.GetUser().ProviderUserKey.ToString();
            List<CustomerOrder> cos = db.customerOrder
                .Where(x => x.MerchantID == MerchantID && (x.RequestStatus == RequestStatus.Booked || x.RequestStatus == RequestStatus.ConfirmedByAdmin))
                .ToList();
            List<Int32> Products = cos.Select(x => x.ProductID).ToList();
            List<ProductAttribute> productAtt = db.productAttributes.Where(x => !x.IsDeleted).ToList().Where(x => Products.Contains(x.ProductID)).ToList();
            ViewBag.ProductAtt = productAtt;
            return PartialView(cos);
        }

        //
        // GET: /Merchants/CustomerOrder/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Merchants/CustomerOrder/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Merchants/CustomerOrder/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Merchants/CustomerOrder/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Merchants/CustomerOrder/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Merchants/CustomerOrder/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Merchants/CustomerOrder/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public JsonResult Confirm(CustomerOrder entity)
        {
            try
            {
                BusinessLayer db = new BusinessLayer();
                CustomerOrder co = db.customerOrder.FirstOrDefault(x => x.CustomerOrderID == entity.CustomerOrderID);
                co.RequestStatus = RequestStatus.ConfirmedByMerchant;
                //co.pohd.DeliveryStatus = DeliveryStatus.Prepared;
                db.Entry(co).State = EntityState.Modified;

                ItemDeliveryHd deliveryHd = db.itemdeliveryhds.FirstOrDefault(x => x.MerchantID == co.MerchantID && x.OrderID == co.OrderID && x.DeliveryStatus == DeliveryStatus.Prepared);
                if (deliveryHd == null)
                {
                    deliveryHd = new ItemDeliveryHd();
                    deliveryHd.MerchantID = co.MerchantID;
                    deliveryHd.OrderID = co.OrderID;
                    deliveryHd.ReceiptNumber = "";
                    deliveryHd.DeliveryDate = DateTime.Now;
                    deliveryHd.CustomerID = co.CustomerID;
                    deliveryHd.Address = co.pohd.Address;
                    deliveryHd.DeliveryStatus = DeliveryStatus.Prepared;
                    deliveryHd.CreatedBy = Membership.GetUser().UserName;
                    deliveryHd.CreatedDate = DateTime.Now;
                    db.itemdeliveryhds.Add(deliveryHd);
                }

                ItemDeliveryDt delivDt = new ItemDeliveryDt();
                delivDt.ProductID = co.ProductID;
                delivDt.Quantity = co.Quantity;
                delivDt.itemdeliveryhd = deliveryHd;
                delivDt.IsDeleted = false;
                delivDt.CreatedBy = Membership.GetUser().UserName;
                delivDt.CreatedDate = DateTime.Now;
                db.itemdeliverydts.Add(delivDt);
                db.SaveChanges();

                return Json(new { ok = true, message = "Success" });
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, message = ex.Message });
            }

        }

        [HttpPost]
        public JsonResult Void(CustomerOrder entity)
        {
            try
            {
                BusinessLayer db = new BusinessLayer();
                CustomerOrder co = db.customerOrder.FirstOrDefault(x => x.CustomerOrderID == entity.CustomerOrderID);
                co.RequestStatus = RequestStatus.Void;

                PurchaseOrderDt podt = co.pohd.orderdetails.FirstOrDefault(x => x.OrderID == co.OrderID && x.cart.ProductID == co.ProductID && !x.IsDeleted);
                podt.OrderStatus = OrderStatus.Void;

                List<PurchaseReceiveDt> prdts = db.purchasereceivedts.Where(x => x.OrderID == co.pohd.OrderID).ToList();
                if (prdts.Count() > 0) 
                {
                    foreach (PurchaseReceiveDt obj in prdts) 
                    {
                        obj.receive.RefundAmount += podt.cart.LineAmount;

                        //if (obj.receive.TransferAmount - obj.receive.RefundAmount == 0)
                        //{
                        //    co.pohd.DeliveryStatus = DeliveryStatus.Void;
                        //}
                    }
                    db.Entry(prdts).State = EntityState.Modified;
                }
                
                db.Entry(co).State = EntityState.Modified;
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
