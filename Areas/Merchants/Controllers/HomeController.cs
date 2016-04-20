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
    [Authorize]
    public class HomeController : Controller
    {
        //
        // GET: /Merchants/Merchants/

        public ActionResult Index()
        {
            BusinessLayer db = new BusinessLayer();
            String Username = Membership.GetUser().UserName;
            List<Merchant> merchants = db.merchants.Where(x => x.userprofile.UserName == Username && x.IsActive == true).ToList();
            return View(merchants);
        }

        //public ActionResult CustomerOrder() 
        //{
        //    Int32 MerchantID = Convert.ToInt32(TempData.Peek("MerchantID"));
        //    BusinessLayer db = new BusinessLayer();
        //    String OwnerID = Membership.GetUser().ProviderUserKey.ToString();
        //    //Merchant merchant = db.merchants.FirstOrDefault(x => x.IsActive && x.userprofile.UserName == OwnerID && x.MerchantID == MerchantID);
        //    List<CustomerOrder> cos = db.customerOrder
        //        .Where(x => x.MerchantID == MerchantID && (x.RequestStatus == RequestStatus.Booked || x.RequestStatus == RequestStatus.ConfirmedByAdmin))
        //        .ToList();
        //    return View(cos);
        //}

        public JsonResult ConfirmRequest(CustomerOrder entity) 
        {
            try
            {
                BusinessLayer db = new BusinessLayer(); 
                CustomerOrder co = db.customerOrder.FirstOrDefault(x =>
                    x.OrderID == entity.OrderID &&
                    x.MerchantID == entity.MerchantID &&
                    x.CustomerID == entity.CustomerID &&
                    x.ProductID == entity.ProductID
                    );
                co.RequestStatus = RequestStatus.ConfirmedByMerchant;
                co.pohd.DeliveryStatus = DeliveryStatus.Prepared;
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
                db.itemdeliverydts.Add(delivDt);
                db.SaveChanges();

                return Json(new { ok = true, message = "Success" });
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, message = ex.Message });
            }
            
        }

        public ActionResult SendPackage() 
        {
            //BusinessLayer db = new BusinessLayer();
            //String OwnerID = Membership.GetUser().UserName;
            //List<Merchant> merchants = db.merchants.Where(x => x.IsActive && x.userprofile.UserName == OwnerID).ToList();
            //List<ItemDeliveryHd> itemdeliveryhds = new List<ItemDeliveryHd>();
            //foreach(Merchant entity in merchants)
            //{
            //    itemdeliveryhds.AddRange(db.itemdeliveryhds.Where(x => x.MerchantID == entity.MerchantID && x.DeliveryStatus == DeliveryStatus.Prepared).ToList());
            //}
            //return View(itemdeliveryhds);
            return View();
        }

        public JsonResult ProcessPackage(ItemDeliveryHd entity) 
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
                return Json(new { ok = true, Status = "Success" });
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, Status = ex.Message });
            }
            
        }

        public JsonResult VoidRequest(CustomerOrder entity) 
        {
            try
            {
                BusinessLayer db = new BusinessLayer();
                //CustomerOrder co = db.customerOrder.FirstOrDefault(x =>
                //    x.OrderID == entity.OrderID &&
                //    x.MerchantID == entity.MerchantID &&
                //    x.CustomerID == entity.CustomerID &&
                //    x.ProductID == entity.ProductID
                //    );
                //co.RequestStatus = RequestStatus.Void;
                
                //PurchaseOrderDt podt = co.pohd.orderdetails.FirstOrDefault(x => x.OrderID == co.OrderID && x.cart.ProductID == co.ProductID);
                //podt.OrderStatus = OrderStatus.Void;
                //if (co.pohd.TotalAmount >= co.pohd.TransferAmount)
                //{
                //    co.pohd.RefundAmount += podt.cart.LineAmount;
                //}

                //if (co.pohd.TransferAmount - co.pohd.RefundAmount == 0) 
                //{
                //    co.pohd.DeliveryStatus = DeliveryStatus.Void;
                //}
                //db.Entry(co).State = EntityState.Modified;
                //db.SaveChanges();
                return Json(new { ok = true, Status = "Success" });
            }
            catch (Exception ex) 
            {
                return Json(new { ok = false, Status = ex.Message });
            }
        }

        public ActionResult Detail(Int32 id) 
        {
            BusinessLayer db = new BusinessLayer();
            Merchant merchant = db.merchants.FirstOrDefault(x => x.MerchantID == id);
            return View(merchant);
        }
    }
}
