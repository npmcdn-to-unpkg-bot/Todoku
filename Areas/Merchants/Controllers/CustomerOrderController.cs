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

        public ActionResult Index()
        {
            BusinessLayer db = new BusinessLayer();
            Int32 MerchantID = Convert.ToInt32(TempData.Peek("MerchantID"));
            Int32 UserID = db.GetUserProfileID();
            List<CustomerOrder> cos = db.customerOrder
                .Where(x => x.MerchantID == MerchantID && x.merchant.OwnerID == UserID && (x.RequestStatus == RequestStatus.Booked || x.RequestStatus == RequestStatus.ConfirmedByAdmin))
                .ToList();
            List<Int32> Products = cos.Select(x => x.ProductID).ToList();
            List<ProductAttribute> productAtt = db.productAttributes.Where(x => !x.IsDeleted).ToList().Where(x => Products.Contains(x.ProductID)).ToList();
            ViewBag.ProductAtt = productAtt;
            return View(cos);
        }

        [HttpPost]
        public JsonResult Confirm(CustomerOrder entity)
        {
            try
            {
                BusinessLayer db = new BusinessLayer();
                CustomerOrder co = db.customerOrder.FirstOrDefault(x => x.CustomerOrderID == entity.CustomerOrderID);
                co.RequestStatus = RequestStatus.ConfirmedByMerchant;
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

                Product product = db.products.Find(co.ProductID);
                product.detail.Quantity -= co.Quantity;
                db.Entry(product).State = EntityState.Modified;

                db.SaveChanges();
                TempData["SaveResult"] = Json(new { ok = true, message = "Data telah berhasil proses" });
                return Json(new { ok = true, message = "Data telah berhasil proses" });
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, message = "Data tidak dapat diproses : " + ex.Message });
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
                        db.Entry(obj).State = EntityState.Modified;
                    }
                }

                db.Entry(co).State = EntityState.Modified;
                db.SaveChanges();
                TempData["SaveResult"] = Json(new { ok = true, message = "Data telah berhasil dihapus" });
                return Json(new { ok = true, message = "Success" });
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, message = ex.Message });
            }
        }
    }
}
