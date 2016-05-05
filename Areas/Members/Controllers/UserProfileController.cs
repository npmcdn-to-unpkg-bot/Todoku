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
    public class UserProfileController : Controller
    {
        //
        // GET: /Members/UserProfile/

        public ActionResult Index()
        {
            BusinessLayer db = new BusinessLayer();
            String Username = Membership.GetUser().UserName;
            UserProfile userprofile = db.userprofiles.Include("shippings").FirstOrDefault(x => x.UserName == Username);
            List<StandardCode> sc = db.standardcodes.Where(x => x.ParentID == SCConstant.Jenis_Kelamin ||
                x.ParentID == SCConstant.Provinsi ||
                x.ParentID == SCConstant.Negara ||
                x.ParentID == SCConstant.Panggilan
                ).ToList();
            if (userprofile != null)
            {
                ViewBag.GenderData = new SelectList(sc.Where(x => x.ParentID == SCConstant.Jenis_Kelamin), "StandardCodeID", "StandardCodeName", userprofile.Gender);
                ViewBag.Province = new SelectList(sc.Where(x => x.ParentID == SCConstant.Provinsi), "StandardCodeID", "StandardCodeName", userprofile.address.Province);
                ViewBag.Country = new SelectList(sc.Where(x => x.ParentID == SCConstant.Negara), "StandardCodeID", "StandardCodeName", userprofile.address.Country);
                ViewBag.PrefixData = new SelectList(sc.Where(x => x.ParentID == SCConstant.Panggilan), "StandardCodeID", "StandardCodeName", userprofile.Prefix);
            }
            else
            {
                ViewBag.Gender = new SelectList(sc.Where(x => x.ParentID == SCConstant.Jenis_Kelamin), "StandardCodeID", "StandardCodeName");
                ViewBag.Province = new SelectList(sc.Where(x => x.ParentID == SCConstant.Provinsi), "StandardCodeID", "StandardCodeName");
                ViewBag.Country = new SelectList(sc.Where(x => x.ParentID == SCConstant.Negara), "StandardCodeID", "StandardCodeName");
                ViewBag.Prefix = new SelectList(sc.Where(x => x.ParentID == SCConstant.Panggilan), "StandardCodeID", "StandardCodeName");
            }

            return View(userprofile);
        }

        [HttpPost, ActionName("Index")]
        public ActionResult Save(UserProfile userprofile)
        {
            BusinessLayer db = new BusinessLayer();
            try
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
                TempData["SaveResult"] = Json(new { ok = true, message = "Data telah berhasil disimpan" });
            }
            catch (Exception ex)
            {
                TempData["SaveResult"] = Json(new { ok = false, message = "Data tidak dapat disimpan : " + ex.Message });
            }
            return RedirectToAction("Index", "UserProfile", new { area = "Members" });
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

        [HttpPost, ActionName("AddShippingAddress")]
        public ActionResult SaveShippingAddress(ShippingAddresses entity) 
        {
            BusinessLayer db = new BusinessLayer();
            try
            {
                entity.UserProfileID = db.GetUserProfileID();
                entity.CreatedBy = Membership.GetUser().UserName;
                entity.CreatedDate = DateTime.Now;
                db.ShippingAddresses.Add(entity);
                db.SaveChanges();
                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                TempData["SaveResult"] = Json(new { ok = false, message = "Data tidak dapat disimpan : " + ex.Message });
                List<StandardCode> sc = db.standardcodes.Where(x => x.ParentID == SCConstant.Provinsi || x.ParentID == SCConstant.Negara).ToList();
                ViewBag.Province = new SelectList(sc.Where(x => x.ParentID == SCConstant.Provinsi), "StandardCodeID", "StandardCodeName");
                ViewBag.Country = new SelectList(sc.Where(x => x.ParentID == SCConstant.Negara), "StandardCodeID", "StandardCodeName");
                Dictionary<String, String> DictProvince = new Dictionary<String, String>();
                foreach (StandardCode s in sc.Where(x => x.ParentID == SCConstant.Provinsi)) DictProvince.Add(s.StandardCodeName, s.Alias);
                ViewBag.provinces = Json(DictProvince);
                return View(entity);
            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteShippingAddressConfirmed(Int32 id)
        {
            BusinessLayer db = new BusinessLayer();
            ShippingAddresses shipping = db.ShippingAddresses.Find(id);
            db.Entry(shipping).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
