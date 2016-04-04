using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Todoku.Models;
using System.Data.Entity.Validation;
using System.Web.Security;
using System.Data;
using System.IO;

namespace Todoku.Areas.Merchants.Controllers
{
    [Authorize]
    public class RegistrationController : Controller
    {
        //
        // GET: /Merchants/Registration/

        public ActionResult Index()
        {
            BusinessLayer db = new BusinessLayer();
            ViewBag.PartialView = "LeftPanel";
            ViewBag.filterExpression = "Merchant";
            List<StandardCode> sc = db.standardcodes.Where(x => x.ParentID == SCConstant.Provinsi || x.ParentID == SCConstant.Negara).ToList();
            sc.Insert(0, new StandardCode { StandardCodeID = "", StandardCodeName = "" });
            ViewBag.Province = new SelectList(sc.Where(x => x.ParentID == SCConstant.Provinsi || x.StandardCodeID == ""), "StandardCodeID", "StandardCodeName");
            ViewBag.Country = new SelectList(sc.Where(x => x.ParentID == SCConstant.Negara || x.StandardCodeID == ""), "StandardCodeID", "StandardCodeName");
            MerchantRegistration regis = new MerchantRegistration();
            regis.details = new List<MerchantRegistrationDetail>();
            ViewBag.RegistrationID = 0;
            return View(regis);
        }

        public ActionResult Register(MerchantRegistration entity)
        {
            try
            {
                if (ModelState.IsValid) 
                {
                    BusinessLayer db = new BusinessLayer();
                    entity.RegistrationCode = Method.GetTransactionCode(db, SystemSetting.RegisMerchantCode);
                    entity.AddressCode = String.Format("{0}{1}", SystemSetting.MerchantCode, entity.RegistrationCode.Substring(3, entity.RegistrationCode.Length - 3));
                    entity.address.AddressCode = entity.AddressCode;
                    entity.RegistrationStatus = RegistrationStatus.Open;

                    String username = Membership.GetUser().UserName;
                    UserProfile up = db.userprofiles.FirstOrDefault(x => x.UserName == username);
                    entity.userprofile = up;
                    entity.RegistrationDate = DateTime.Now;

                    entity.CreatedBy = username;
                    entity.CreatedDate = DateTime.Now;

                    db.merchantRegistrations.Add(entity);
                    db.SaveChanges();

                    entity.RegistrationID = db.merchantRegistrations.Max(item => item.RegistrationID);
                    ViewBag.RegistrationID = entity.RegistrationID;
                    List<StandardCode> sc = db.standardcodes.Where(x => x.ParentID == SCConstant.Provinsi || x.ParentID == SCConstant.Negara).ToList();
                    sc.Insert(0, new StandardCode { StandardCodeID = "", StandardCodeName = "" });
                    ViewBag.Province = new SelectList(sc.Where(x => x.ParentID == SCConstant.Provinsi || x.StandardCodeID == ""), "StandardCodeID", "StandardCodeName", entity.address.Province);
                    ViewBag.Country = new SelectList(sc.Where(x => x.ParentID == SCConstant.Negara || x.StandardCodeID == ""), "StandardCodeID", "StandardCodeName", entity.address.Country);
                    entity.details = db.merchantRegistrationDetails.Where(x => x.RegistrationID == entity.RegistrationID).ToList();
                }
                
                ViewBag.PartialView = "LeftPanel";
                ViewBag.filterExpression = "Merchant";
                
                return View("Index", entity);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Registration");
            }
        }

        [HttpPost]
        public ActionResult UploadFile(SampleProduct product)
        {
            BusinessLayer db = new BusinessLayer();
            Boolean Status = true;
            String errMessage = "";
            try
            {
                MerchantRegistration mr = db.merchantRegistrations.Find(product.RegistrationID);
                if (mr.StartPrice != product.StartPrice || mr.EndPrice != product.EndPrice) 
                {
                    mr.StartPrice = product.StartPrice;
                    mr.EndPrice = product.EndPrice;
                    mr.LastUpdatedBy = Membership.GetUser().UserName;
                    mr.LastUpdatedDate = DateTime.Now;
                    db.Entry(mr).State = EntityState.Modified;
                }
                
                if (Method.UploadFileValidation(product.file, 1024 * 100, new String[] { ".jpg" })) 
                {
                    String MerchantFolder = Path.Combine(Server.MapPath(SystemSetting.Default_Upload_Registration), mr.RegistrationCode);

                    if (!Directory.Exists(MerchantFolder)) Directory.CreateDirectory(MerchantFolder);

                    var path = Path.Combine(MerchantFolder, product.file.FileName);
                    product.file.SaveAs(path);

                    MerchantRegistrationDetail detail = new MerchantRegistrationDetail();
                    detail.ProductName = product.ProductName;
                    detail.Description = product.ProductDescription;
                    detail.ImgLink = String.Format(@"{0}{1}\{2}", SystemSetting.Default_Upload_Registration, mr.RegistrationCode, product.file.FileName);
                    detail.RegistrationID = product.RegistrationID;
                    detail.CreatedBy = Membership.GetUser().UserName;
                    detail.CreatedDate = DateTime.Now;
                    db.merchantRegistrationDetails.Add(detail);
                }
                
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Status = false;
                errMessage = ex.Message;
            }
            ViewBag.PartialView = "LeftPanel";
            ViewBag.filterExpression = "Merchant";

            MerchantRegistration entity = db.merchantRegistrations.Include("details").FirstOrDefault(x => x.RegistrationID == product.RegistrationID);
            if (entity != null)
            {
                List<StandardCode> sc = db.standardcodes.Where(x => x.ParentID == SCConstant.Provinsi || x.ParentID == SCConstant.Negara).ToList();
                sc.Insert(0, new StandardCode { StandardCodeID = "", StandardCodeName = "" });
                ViewBag.Province = new SelectList(sc.Where(x => x.ParentID == SCConstant.Provinsi || x.StandardCodeID == ""), "StandardCodeID", "StandardCodeName", entity.address.Province);
                ViewBag.Country = new SelectList(sc.Where(x => x.ParentID == SCConstant.Negara || x.StandardCodeID == ""), "StandardCodeID", "StandardCodeName", entity.address.Country);
            }
            else 
            {
                entity = new MerchantRegistration();
                entity.details = new List<MerchantRegistrationDetail>();
                List<StandardCode> sc = db.standardcodes.Where(x => x.ParentID == SCConstant.Provinsi || x.ParentID == SCConstant.Negara).ToList();
                sc.Insert(0, new StandardCode { StandardCodeID = "", StandardCodeName = "" });
                ViewBag.Province = new SelectList(sc.Where(x => x.ParentID == SCConstant.Provinsi || x.StandardCodeID == ""), "StandardCodeID", "StandardCodeName");
                ViewBag.Country = new SelectList(sc.Where(x => x.ParentID == SCConstant.Negara || x.StandardCodeID == ""), "StandardCodeID", "StandardCodeName");
            }
            return View("Index", entity);
        }

        [HttpPost]
        public ActionResult SubmitRegistration(int RegistrationID) 
        {
            BusinessLayer db = new BusinessLayer();
            MerchantRegistration mr = db.merchantRegistrations.Find(RegistrationID);
            mr.RegistrationStatus = RegistrationStatus.Request;
            mr.LastUpdatedBy = Membership.GetUser().UserName;
            mr.LastUpdatedDate = DateTime.Now;
            db.Entry(mr).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
