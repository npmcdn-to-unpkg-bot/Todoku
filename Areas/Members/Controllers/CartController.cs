using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Todoku.Models;
using System.Web.Security;
using System.Web.Script.Serialization;
using System.Data;

namespace Todoku.Areas.Members.Controllers
{
    public class CartController : Controller
    {
        //
        // GET: /Members/Cart/

        public ActionResult Index()
        {
            BusinessLayer db = new BusinessLayer();
            List<Cart> carts = null;
            if (User.Identity.IsAuthenticated)
            {
                String Username = Membership.GetUser().UserName;
                carts = db.carts.Where(x => x.Username == Username && x.ItemStatus == ItemStatus.Requested).ToList();
            }
            else
            {
                carts = AppSession.GetCartUsingCookie(this.HttpContext);
            }
            List<ProductAttribute> productAtt = db.productAttributes.Where(x => !x.IsDeleted).ToList();
            ViewBag.ProductAtt = productAtt;
            ViewBag.TotalAmount = carts.Sum(x => x.LineAmount);
            return View(carts);
        }

        [HttpPost, ActionName("Index")]
        public ActionResult Delete(Int32 id) 
        {
            BusinessLayer db = new BusinessLayer();
            Cart cart = db.carts.Find(id);
            db.Entry(cart).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpPost]
        public JsonResult AddToCart(Cart cart)
        {
            BusinessLayer db = new BusinessLayer();
            try
            {
                if (ModelState.IsValid)
                {
                    String UserName = "";
                    Product product = db.products.Find(cart.ProductID);

                    if (User.Identity.IsAuthenticated)
                    {
                        UserName = Membership.GetUser().UserName;
                        Cart entity = db.carts.FirstOrDefault(x => x.Username == UserName && x.ProductID == cart.ProductID && x.Attributes == cart.Attributes && x.ItemStatus == ItemStatus.Requested);
                        if (entity == null)
                        {
                            entity = new Cart();
                            entity.ProductID = cart.ProductID;
                            entity.Quantity = cart.Quantity;
                            entity.TotalAmount = product.detail.LineAmount;
                            entity.DiscountAmount = product.detail.DiscountAmount + product.detail.DiscountAmount2 + product.detail.DiscountAmount3;
                            entity.DiscountInPercentage = product.detail.DiscountInPercentage;
                            entity.Attributes = cart.Attributes;
                            //entity.LineAmount = cart.Quantity * product.detail.LineAmount;
                            entity.CreatedDate = DateTime.Now;
                            entity.Username = UserName;
                            entity.ItemStatus = ItemStatus.Requested;
                            db.carts.Add(entity);
                        }
                        else
                        {
                            entity.Quantity += cart.Quantity;
                            //entity.LineAmount = entity.Quantity * product.detail.LineAmount;
                            db.Entry(entity).State = EntityState.Modified;
                        }
                        db.SaveChanges();
                    }
                    else
                    {
                        List<Cart> carts = AppSession.GetCartUsingCookie(this.HttpContext);
                        if (carts == null) carts = new List<Cart>();

                        Cart entity = carts != null ? carts.FirstOrDefault(x => x.Username == UserName && x.Attributes == cart.Attributes && x.ProductID == cart.ProductID) : null;
                        if (entity == null)
                        {
                            entity = new Cart();
                            entity.CartID = carts.Count() + 1;
                            entity.ProductID = cart.ProductID;
                            entity.product = new Product();
                            entity.product.ProductName = product.ProductName;
                            entity.product.detail = new ProductDt();
                            entity.product.detail.LineAmount = product.detail.LineAmount;
                            entity.Quantity = cart.Quantity;
                            entity.Attributes = String.Join("|", cart.Attributes);
                            entity.TotalAmount = product.detail.LineAmount;
                            //entity.LineAmount = cart.Quantity * product.detail.Price;
                            entity.CreatedDate = DateTime.Now;
                            entity.Username = UserName;
                            entity.ItemStatus = ItemStatus.Requested;
                            entity.product.ImgLink = product.ImgLink;
                            carts.Add(entity);
                        }
                        else
                        {
                            entity.Quantity += cart.Quantity;
                            //entity.LineAmount = entity.Quantity * product.detail.Price;
                        }
                        AppSession.SetCartUsingCookie(this.HttpContext, carts);
                    }
                }
                return Json(new { ok = true, message = "Success" });
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, message = ex.Message });
            }

        }

        [Authorize]
        [HttpPost, ActionName("Process")]
        public ActionResult ProcessCart()
        {
            try
            {
                #region Create PO
                BusinessLayer db = new BusinessLayer();
                String UserName = Membership.GetUser().UserName;
                String[] roles = Roles.GetRolesForUser(Membership.GetUser().UserName);
                String OrderNo = "";

                String DateTransaction = String.Format("{0}{1}{2}", DateTime.Now.Year, DateTime.Now.Month.ToString("00"), DateTime.Now.Day.ToString("00"));
                Int32 count = db.purchaseorderhds.Where(x => x.OrderNo.Contains(DateTransaction)).Count() + 1;
                
                if (roles.Any(x => x.ToLower().Contains("member")))
                {
                    UserProfile up = db.userprofiles.FirstOrDefault(x => x.UserName == UserName);
                    List<Cart> carts = db.carts.Where(x => x.Username == UserName && x.ItemStatus == ItemStatus.Requested).ToList();

                    List<Int32> lstMerchantID = carts.GroupBy(x => x.product.merchant.MerchantID).Select(x => x.Key).ToList();
                    foreach (Int32 merchantID in lstMerchantID)
                    {
                        #region Create PO
                        PurchaseOrderHd pohd = new PurchaseOrderHd();
                        pohd.CustomerID = up.UserProfileID;
                        pohd.AgentID = null;
                        pohd.OrderDate = DateTime.Now;
                        pohd.TotalAmount = carts.Where(x => x.product.MerchantID == merchantID).Sum(x => x.LineAmount);
                        pohd.MerchantID = merchantID;
                        pohd.ShippingCharges = 0;
                        pohd.InsuranceCharges = 0;
                        pohd.CreatedDate = DateTime.Now;
                        
                        pohd.OrderNo = String.Format("{0}/{1}/{2}", TransactionNoPrefix.Purchase_Order, DateTransaction, count.ToString("000000"));
                        if (OrderNo == "") OrderNo = pohd.OrderNo;
                        pohd.PaymentMehod = PaymentMethod.TRANSFER;
                        pohd.ValidUntil = DateTime.Now.AddDays(SystemSetting.ValidUntil);
                        pohd.OrderStatus = OrderStatus.Open;
                        pohd.CreatedBy = Membership.GetUser().UserName;
                        pohd.CreatedDate = DateTime.Now;
                        db.purchaseorderhds.Add(pohd);

                        foreach (Cart cart in carts.Where(x => x.product.MerchantID == merchantID))
                        {
                            PurchaseOrderDt podt = new PurchaseOrderDt();
                            podt.order = pohd;
                            podt.CartID = cart.CartID;
                            db.purchaseorderdts.Add(podt);
                            cart.ItemStatus = ItemStatus.Ordered;
                            podt.CreatedBy = Membership.GetUser().UserName;
                            podt.CreatedDate = DateTime.Now;
                            db.Entry(cart).State = EntityState.Modified;

                            CustomerOrder co = new CustomerOrder();
                            co.MerchantID = cart.product.MerchantID;
                            co.pohd = pohd;
                            co.ProductID = cart.ProductID;
                            co.Attributes = cart.Attributes;
                            co.Quantity = cart.Quantity;
                            co.RequestStatus = RequestStatus.Booked;
                            co.CustomerID = up.UserProfileID;
                            db.customerOrder.Add(co);
                        }
                        #endregion
                        count++;
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index", "Cart");
                }
                #endregion
                
                return RedirectToAction("Register", "Account", new { area = "Members" });
            }
            catch (Exception ex)
            {
                TempData["SaveResult"] = Json(new { ok = false, message = ex.Message });
                return RedirectToAction("Index", "Cart");
            }
        }
    }
}
