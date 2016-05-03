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
    public class old_HomeController : Controller
    {
        //
        // GET: /User/
        [Authorize]
        public ActionResult Index()
        {
            BusinessLayer db = new BusinessLayer();
            String UserID = Membership.GetUser().UserName;
            UserProfile userprofile = db.userprofiles.Include("shippings").FirstOrDefault(x => x.UserName == UserID);
            List<StandardCode> sc = db.standardcodes.Where(x => x.ParentID == SCConstant.Jenis_Kelamin ||
                x.ParentID == SCConstant.Provinsi ||
                x.ParentID == SCConstant.Negara ||
                x.ParentID == SCConstant.Panggilan
                ).ToList();
            if (userprofile != null)
            {
                ViewBag.Gender = new SelectList(sc.Where(x => x.ParentID == SCConstant.Jenis_Kelamin), "StandardCodeID", "StandardCodeName", userprofile.Gender);
                ViewBag.Province = new SelectList(sc.Where(x => x.ParentID == SCConstant.Provinsi), "StandardCodeID", "StandardCodeName", userprofile.address.Province);
                ViewBag.Country = new SelectList(sc.Where(x => x.ParentID == SCConstant.Negara), "StandardCodeID", "StandardCodeName", userprofile.address.Country);
                ViewBag.Prefix = new SelectList(sc.Where(x => x.ParentID == SCConstant.Panggilan), "StandardCodeID", "StandardCodeName", userprofile.Prefix);
            }
            else {
                ViewBag.Gender = new SelectList(sc.Where(x => x.ParentID == SCConstant.Jenis_Kelamin), "StandardCodeID", "StandardCodeName");
                ViewBag.Province = new SelectList(sc.Where(x => x.ParentID == SCConstant.Provinsi), "StandardCodeID", "StandardCodeName");
                ViewBag.Country = new SelectList(sc.Where(x => x.ParentID == SCConstant.Negara), "StandardCodeID", "StandardCodeName");
                ViewBag.Prefix = new SelectList(sc.Where(x => x.ParentID == SCConstant.Panggilan), "StandardCodeID", "StandardCodeName");
            }
            
            return View(userprofile);
        }

        [HttpPost]
        public ActionResult Edit(UserProfile userprofile)
        {
            BusinessLayer db = new BusinessLayer();
            try
            {
                if (ModelState.IsValid)
                {
                    String UserName = Membership.GetUser().UserName;
                    UserProfile entity = db.userprofiles.FirstOrDefault(x => x.UserName == UserName);
                    entity.Fullname = userprofile.Fullname;
                    entity.Gender = userprofile.Gender;
                    entity.Prefix = userprofile.Prefix;
                    entity.DateOfBirth = userprofile.DateOfBirth;
                    entity.address.Country = userprofile.address.Country;
                    entity.address.Province = userprofile.address.Province;
                    entity.address.City = userprofile.address.City;
                    entity.address.Address = userprofile.address.Address;
                    entity.address.ZipCode = userprofile.address.ZipCode;
                    entity.address.Email = userprofile.address.Email;
                    entity.address.Email2 = userprofile.address.Email2;
                    entity.address.Handphone = userprofile.address.Handphone;
                    entity.address.Phone = userprofile.address.Phone;
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home", new { area = "Members" });
                }
            }
            catch (Exception ex)
            {
                String errMessage = ex.Message;
            }

            return RedirectToAction("Index", "UserProfile");
        }

        public ActionResult OrderConfirmation()
        {
            BusinessLayer db = new BusinessLayer();
            String username = Membership.GetUser().UserName;
            UserProfile cust = db.userprofiles.FirstOrDefault(x => x.UserName == username);
            List<PurchaseOrderHd> lst = null;
            if (cust != null)
            {
                lst = db.purchaseorderhds.Where(x => x.CustomerID == cust.UserProfileID && x.OrderStatus == OrderStatus.Open).ToList();
            }

            return View(lst);
        }

        public ActionResult PaymentConfirmation() 
        {
            BusinessLayer db = new BusinessLayer(); 
            String UserName = Membership.GetUser().UserName;
            UserProfile up = db.userprofiles.FirstOrDefault(x => x.UserName == UserName);
            List<PurchaseReceiveHd> prhd = db.purchasereceivehds.Where(x => x.CustomerID == up.UserProfileID && x.ReceiveStatus == ReceiveStatus.Open).ToList();
            return View();
        }
        
        public ActionResult MerchantRegistration()
        {
            BusinessLayer db = new BusinessLayer();
            List<MerchantRegistration> merchants = db.merchantRegistrations.Where(x => x.RegistrationStatus == RegistrationStatus.Request).ToList();
            return View(merchants);
        }

        [HttpPost]
        public JsonResult MerchantConfirmation(MerchantRegistration entity)
        {
            try
            {
                BusinessLayer db = new BusinessLayer();
                MerchantRegistration obj = db.merchantRegistrations.FirstOrDefault(x => x.RegistrationID == entity.RegistrationID);
                obj.RegistrationStatus = RegistrationStatus.ConfirmedByAdmin;
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();

                return Json(new { ok = true, message = "Success" });
            }
            catch(Exception ex) 
            {
                return Json(new { ok = false, message = ex.Message });
            }
        }
        
        public ActionResult OrderDetail(Int32 ID) 
        {
            BusinessLayer db = new BusinessLayer();
            String UserName = Membership.GetUser().UserName;
            UserProfile up = db.userprofiles.FirstOrDefault(x => x.UserName == UserName);
            PurchaseOrderHd pohd = db.purchaseorderhds.FirstOrDefault(x => x.OrderID == ID && x.CustomerID == up.UserProfileID && x.OrderStatus == OrderStatus.Open);
            List<StandardCode> lst = db.standardcodes.Where(x => x.ParentID == SCConstant.Cara_Pembayaran).ToList();
            ViewBag.Banks = db.banks.ToList();
            ViewBag.PaymentMethod = new SelectList(lst, "StandardCodeID", "StandardCodeName");
            ViewBag.TotalWeight = pohd.orderdetails.Sum(x => x.cart.product.detail.Weight);
            List<ProductAttribute> productAtt = db.productAttributes.Where(x => !x.IsDeleted).ToList();
            ViewBag.ProductAtt = productAtt;
            pohd.Address = up.address.Address;
            return View(pohd);
        }

        public ActionResult AddShippingAddress() 
        {
            BusinessLayer db = new BusinessLayer();
            List<StandardCode> sc = db.standardcodes.Where(x => x.ParentID == SCConstant.Provinsi || x.ParentID == SCConstant.Negara).ToList();
            ViewBag.Province = new SelectList(sc.Where(x => x.ParentID == SCConstant.Provinsi), "StandardCodeID", "StandardCodeName");
            ViewBag.Country = new SelectList(sc.Where(x => x.ParentID == SCConstant.Negara), "StandardCodeID", "StandardCodeName");
            Dictionary<String, String> DictProvince = new Dictionary<String, String>();
            foreach (StandardCode s in sc.Where(x => x.ParentID == SCConstant.Provinsi)) DictProvince.Add(s.StandardCodeName, s.Alias);
            ViewBag.provinces = Json(DictProvince);
            return View();
        }

        [HttpPost]
        public ActionResult AddShippingAddress(ShippingAddresses entity) 
        {
            try
            {
                BusinessLayer db = new BusinessLayer();
                String Username = Membership.GetUser().UserName;
                UserProfile up = db.userprofiles.FirstOrDefault(x => x.UserName == Username);
                entity.UserProfileID = up.UserProfileID;
                entity.CreatedBy = Membership.GetUser().UserName;
                entity.CreatedDate = DateTime.Now;
                db.ShippingAddresses.Add(entity);
                db.SaveChanges();
                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        public ActionResult EditShippingAddress(Int32 id) 
        {
            BusinessLayer db = new BusinessLayer();
            ShippingAddresses shipping = db.ShippingAddresses.Find(id);
            List<StandardCode> sc = db.standardcodes.Where(x => x.ParentID == SCConstant.Provinsi || x.ParentID == SCConstant.Negara).ToList();
            ViewBag.Province = new SelectList(sc.Where(x => x.ParentID == SCConstant.Provinsi), "StandardCodeID", "StandardCodeName", shipping.Province);
            ViewBag.Country = new SelectList(sc.Where(x => x.ParentID == SCConstant.Negara), "StandardCodeID", "StandardCodeName", shipping.Country);
            Dictionary<String, String> DictProvince = new Dictionary<String, String>();
            foreach (StandardCode s in sc.Where(x => x.ParentID == SCConstant.Provinsi)) DictProvince.Add(s.StandardCodeName, s.Alias);
            ViewBag.provinces = Json(DictProvince);
            return View(shipping);
        }

        [HttpPost]
        public ActionResult EditShippingAddress(ShippingAddresses entity) 
        {
            BusinessLayer db = new BusinessLayer();
            try
            {
                ShippingAddresses obj = db.ShippingAddresses.Find(entity.ShippingID);
                obj.AddressName = entity.AddressName;
                obj.ZipCode = entity.ZipCode;
                obj.Country = entity.Country;
                obj.Province = entity.Province;
                obj.RajaOngkir_Province_ID = entity.RajaOngkir_Province_ID;
                obj.City = entity.City;
                obj.RajaOngkir_City_ID = entity.RajaOngkir_City_ID;
                obj.Address = entity.Address;
                obj.Email = entity.Email;
                obj.Email2 = entity.Email2;
                obj.Phone = entity.Phone;
                obj.Handphone = entity.Handphone;
                obj.LastUpdatedBy = Membership.GetUser().UserName;
                obj.LastUpdatedDate = DateTime.Now;
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                List<StandardCode> sc = db.standardcodes.Where(x => x.ParentID == SCConstant.Provinsi || x.ParentID == SCConstant.Negara).ToList();
                ViewBag.Province = new SelectList(sc.Where(x => x.ParentID == SCConstant.Provinsi), "StandardCodeID", "StandardCodeName");
                ViewBag.Country = new SelectList(sc.Where(x => x.ParentID == SCConstant.Negara), "StandardCodeID", "StandardCodeName");
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        public ActionResult DeleteShippingAddress(Int32 id) 
        {
            BusinessLayer db = new BusinessLayer();
            ShippingAddresses shipping = db.ShippingAddresses.Find(id);
            return View(shipping);
        }

        [HttpPost, ActionName("DeleteShippingAddress")]
        public ActionResult DeleteShippingAddressConfirmed (Int32 id)
        {
            BusinessLayer db = new BusinessLayer();
            ShippingAddresses shipping = db.ShippingAddresses.Find(id);
            db.Entry(shipping).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
