using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Todoku.Models;
using System.Web.Security;
using System.Data;

namespace Todoku.Areas.Members.Controllers
{
    public class CartController : Controller
    {
        //
        // GET: /Cart/

        public ActionResult Index()
        {
            BusinessLayer db = new BusinessLayer();
            String UserID = "";
            List<Cart> carts = null;
            if (User.Identity.IsAuthenticated)
            {
                UserID = Membership.GetUser().UserName;
                carts = db.carts.Where(x => x.UserName == UserID && x.ItemStatus == ItemStatus.Requested).ToList();
            }
            else 
            {
                carts = AppSession.GetCartUsingCookie(this.HttpContext);
            }
            return View(carts);
        }

        [HttpPost]
        public ActionResult AddToCart(Cart cart)
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
                        Cart entity = db.carts.FirstOrDefault(x => x.UserName == UserName && x.ProductID == cart.ProductID && x.ItemStatus == ItemStatus.Requested);
                        if (entity == null)
                        {
                            entity = new Cart();
                            entity.ProductID = cart.ProductID;
                            entity.Quantity = cart.Quantity;
                            entity.DiscountAmount = product.detail.DiscountAmount + product.detail.DiscountAmount2 + product.detail.DiscountAmount3;
                            entity.DiscountInPercentage = product.detail.DiscountInPercentage;
                            entity.LineAmount = cart.Quantity * product.detail.LineAmount;
                            entity.CreatedDate = DateTime.Now;
                            entity.UserName = UserName;
                            entity.ItemStatus = ItemStatus.Requested;
                            db.carts.Add(entity);
                        }
                        else
                        {
                            entity.Quantity += cart.Quantity;
                            entity.LineAmount = entity.Quantity * product.detail.LineAmount;
                            db.Entry(entity).State = EntityState.Modified;
                        }
                        db.SaveChanges();
                    }
                    else
                    {
                        List<Cart> carts = AppSession.GetCartUsingCookie(this.HttpContext);
                        if (carts == null) carts = new List<Cart>();

                        Cart entity = carts != null ? carts.FirstOrDefault(x => x.UserName == UserName && x.ProductID == cart.ProductID) : null;
                        if (entity == null)
                        {
                            entity = new Cart();
                            entity.CartID = carts.Count() + 1;
                            entity.ProductID = cart.ProductID;
                            entity.product = new Product();
                            entity.product.ProductName = product.ProductName;
                            entity.product.detail = new ProductsDetails();
                            entity.product.detail.LineAmount = product.detail.LineAmount;
                            entity.Quantity = cart.Quantity;
                            entity.LineAmount = cart.Quantity * product.detail.Price;
                            entity.CreatedDate = DateTime.Now;
                            entity.UserName = UserName;
                            entity.ItemStatus = ItemStatus.Requested;
                            entity.product.ImgLink = product.ImgLink;
                            carts.Add(entity);
                        }
                        else
                        {
                            entity.Quantity += cart.Quantity;
                            entity.LineAmount = entity.Quantity * product.detail.Price;
                        }
                        AppSession.SetCartUsingCookie(this.HttpContext, carts);
                    }
                }
            }
            catch (Exception ex)
            {
                String errMessage = ex.Message;
            }
            return RedirectToAction("Detail", "Product", new { area="Stores", id = cart.ProductID });
        }

        [Authorize]
        public ActionResult ProcessCart()
        {
            try
            {
                #region Create PO
                BusinessLayer db = new BusinessLayer();
                String UserName = Membership.GetUser().UserName;
                String[] roles = Roles.GetRolesForUser(Membership.GetUser().UserName);
                if (roles.Any(x => x.ToLower().Contains("member")))
                {
                    UserProfile up = db.userprofiles.FirstOrDefault(x => x.UserName == UserName);
                    List<Cart> carts = db.carts.Where(x => x.UserName == UserName && x.ItemStatus == ItemStatus.Requested).ToList();

                    PurchaseOrderHd pohd = new PurchaseOrderHd();
                    pohd.CustomerID = up.UserProfileID;
                    pohd.AgentID = null;
                    pohd.TotalAmount = carts.Sum(x => x.LineAmount);
                    pohd.CreatedDate = DateTime.Now;
                    String DateTransaction = String.Format("{0}{1}{2}", DateTime.Now.Year, DateTime.Now.Month.ToString("00"), DateTime.Now.Day.ToString("00"));
                    Int32 count = db.purchaseorderhds.Where(x => x.OrderNo.Contains(DateTransaction)).Count() + 1;
                    pohd.OrderNo = String.Format("{0}/{1}/{2}", TransactionNoPrefix.Purchase_Order, DateTransaction, count.ToString("000000"));
                    pohd.PaymentMehod = PaymentMethod.TRANSFER;
                    pohd.ValidUntil = DateTime.Now.AddDays(SystemSetting.ValidUntil);
                    pohd.OrderStatus = OrderStatus.Open;
                    pohd.CreatedBy = Membership.GetUser().UserName;
                    pohd.CreatedDate = DateTime.Now;
                    db.purchaseorderhds.Add(pohd);

                    foreach (Cart cart in carts)
                    {
                        PurchaseOrderDt podt = new PurchaseOrderDt();
                        podt.order = pohd;
                        podt.CartID = cart.CartID;
                        db.purchaseorderdts.Add(podt);
                        cart.ItemStatus = ItemStatus.Ordered;
                        db.Entry(cart).State = EntityState.Modified;

                        CustomerOrder co = new CustomerOrder();
                        co.MerchantID = cart.product.MerchantID;
                        co.pohd = pohd;
                        co.ProductID = cart.ProductID;
                        co.Quantity = cart.Quantity;
                        co.RequestStatus = RequestStatus.Booked;
                        co.CustomerID = up.UserProfileID;
                        db.customerOrder.Add(co);
                    }

                    db.SaveChanges();
                    return RedirectToAction("Index", "PurchaseOrder", new { OrderNo = pohd.OrderNo });
                }
                #endregion
                return RedirectToAction("Index", "PurchaseOrder");
            }
            catch (Exception ex) 
            {
                return RedirectToAction("Index", "Cart");
            }
        }

        [HttpPost]
        public JsonResult RemoveFromCart(Cart cart) 
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    BusinessLayer db = new BusinessLayer();
                    Cart entity = db.carts.Find(cart.CartID);
                    db.Entry(entity).State = EntityState.Deleted;
                    db.SaveChanges();
                }
                else
                {
                    List<Cart> carts = AppSession.GetCartUsingCookie(this.HttpContext);
                    Cart entity = carts.FirstOrDefault(x => x.CartID == cart.CartID);
                    carts.Remove(entity);
                    AppSession.SetCartUsingCookie(this.HttpContext, carts);
                }

                return Json(new { ok = true, message = "Success" });
            }
            catch (Exception ex) 
            {
                return Json(new { ok = false, message = ex.Message });
            }
        }
    }
}
