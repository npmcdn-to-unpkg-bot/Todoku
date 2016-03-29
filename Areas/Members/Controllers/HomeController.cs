﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Todoku.Models;
using System.Web.Security;
using System.Data;

namespace Todoku.Areas.Members.Controllers
{
    public class HomeController : Controller
    {
        BusinessLayer db = new BusinessLayer();

        //
        // GET: /User/
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.PartialView = "LeftPanel";
            ViewBag.filterExpression = "Member";
            String UserID = Membership.GetUser().UserName;
            UserProfile userprofile = db.userprofiles.Include("shippings").FirstOrDefault(x => x.UserName == UserID);
            List<StandardCode> sc = db.standardcodes.Where(x => x.ParentID == SCConstant.Jenis_Kelamin ||
                x.ParentID == SCConstant.Provinsi ||
                x.ParentID == SCConstant.Negara ||
                x.ParentID == SCConstant.Panggilan
                ).ToList();
            ViewBag.Gender = new SelectList(sc.Where(x => x.ParentID == SCConstant.Jenis_Kelamin), "StandardCodeID", "StandardCodeName", userprofile.Gender);
            ViewBag.Province = new SelectList(sc.Where(x => x.ParentID == SCConstant.Provinsi), "StandardCodeID", "StandardCodeName", userprofile.address.Province);
            ViewBag.Country = new SelectList(sc.Where(x => x.ParentID == SCConstant.Negara), "StandardCodeID", "StandardCodeName", userprofile.address.Country);
            ViewBag.Prefix = new SelectList(sc.Where(x => x.ParentID == SCConstant.Panggilan), "StandardCodeID", "StandardCodeName", userprofile.Prefix);
            return View(userprofile);
        }

        [HttpPost]
        public ActionResult Edit(UserProfile userprofile)
        {
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
            ViewBag.PartialView = "LeftPanel";
            ViewBag.filterExpression = "Member";
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
            ViewBag.PartialView = "LeftPanel";
            ViewBag.filterExpression = "Member";
            String UserName = Membership.GetUser().UserName;
            UserProfile up = db.userprofiles.FirstOrDefault(x => x.UserName == UserName);
            List<PurchaseOrderHd> pohds = db.purchaseorderhds.Where(x => x.CustomerID == up.UserProfileID && x.OrderStatus == OrderStatus.Order).ToList();
            return View(pohds);
        }
        
        public ActionResult MerchantRegistration()
        {
            ViewBag.PartialView = "LeftPanel";
            ViewBag.filterExpression = "Member";
            BusinessLayer db = new BusinessLayer();
            List<Merchant> merchants = db.merchants.Where(x => x.RegistrationStatus == RegistrationStatus.Request).ToList();
            return View(merchants);
        }

        [HttpPost]
        public JsonResult MerchantConfirmation(Merchant entity)
        {
            try
            {
                BusinessLayer db = new BusinessLayer();
                Merchant obj = db.merchants.FirstOrDefault(x => x.MerchantID == entity.MerchantID);
                obj.IsActive = true;
                obj.JoinDate = DateTime.Now;
                obj.RegistrationStatus = RegistrationStatus.Approved;
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
            ViewBag.PartialView = "LeftPanel";
            ViewBag.filterExpression = "Member";
            BusinessLayer db = new BusinessLayer();
            String UserName = Membership.GetUser().UserName;
            UserProfile up = db.userprofiles.FirstOrDefault(x => x.UserName == UserName);
            PurchaseOrderHd pohd = db.purchaseorderhds.FirstOrDefault(x => x.OrderID == ID && x.CustomerID == up.UserProfileID && x.OrderStatus == OrderStatus.Open);
            List<StandardCode> lst = db.standardcodes.Where(x => x.ParentID == SCConstant.Cara_Pembayaran).ToList();
            ViewBag.Banks = db.banks.ToList();
            ViewBag.PaymentMethod = new SelectList(lst, "StandardCodeID", "StandardCodeName");
            pohd.Address = up.address.Address;
            return View(pohd);
        }

        public ActionResult AddShippingAddress() 
        {
            List<StandardCode> sc = db.standardcodes.Where(x => x.ParentID == SCConstant.Provinsi || x.ParentID == SCConstant.Negara).ToList();
            ViewBag.Province = new SelectList(sc.Where(x => x.ParentID == SCConstant.Provinsi), "StandardCodeID", "StandardCodeName");
            ViewBag.Country = new SelectList(sc.Where(x => x.ParentID == SCConstant.Negara), "StandardCodeID", "StandardCodeName");
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
            ViewBag.Province = new SelectList(sc.Where(x => x.ParentID == SCConstant.Provinsi), "StandardCodeID", "StandardCodeName");
            ViewBag.Country = new SelectList(sc.Where(x => x.ParentID == SCConstant.Negara), "StandardCodeID", "StandardCodeName");
            return View(shipping);
        }

        [HttpPost]
        public ActionResult EditShippingAddress(ShippingAddresses entity) 
        {
            try
            {
                BusinessLayer db = new BusinessLayer();
                entity.LastUpdatedBy = Membership.GetUser().UserName;
                entity.LastUpdatedDate = DateTime.Now;
                db.Entry(entity).State = EntityState.Modified;
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