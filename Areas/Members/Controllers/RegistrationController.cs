using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Todoku.Models;
using System.Web.Security;
using System.Data;
using System.IO;

namespace Todoku.Areas.Members.Controllers
{
    public class RegistrationController : Controller
    {
        //
        // GET: /Members/Registration/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Merchant()
        {
            BusinessLayer db = new BusinessLayer();
            List<StandardCode> sc = db.standardcodes.Where(x => x.ParentID == SCConstant.Provinsi || x.ParentID == SCConstant.Negara).ToList();
            sc.Insert(0, new StandardCode { StandardCodeID = "", StandardCodeName = "" });
            ViewBag.Province = new SelectList(sc.Where(x => x.ParentID == SCConstant.Provinsi || x.StandardCodeID == ""), "StandardCodeID", "StandardCodeName");
            ViewBag.Country = new SelectList(sc.Where(x => x.ParentID == SCConstant.Negara || x.StandardCodeID == ""), "StandardCodeID", "StandardCodeName");
            Dictionary<String, String> DictProvince = new Dictionary<String, String>();
            foreach (StandardCode s in sc.Where(x => x.ParentID == SCConstant.Provinsi)) DictProvince.Add(s.StandardCodeName, s.Alias);
            ViewBag.provinces = Json(DictProvince);

            MerchantRegistrationEntry entity = new MerchantRegistrationEntry();
            entity.sample = new List<SampleProduct>();
            entity.sample.Add(new SampleProduct { ProductName="test", ProductDescription = "" });
            entity.sample.Add(new SampleProduct { ProductName = "test123", ProductDescription = "" });

            return View(entity);
        }

        [HttpPost, ActionName("Merchant")]
        public ActionResult MerchantRegistration(MerchantRegistrationEntry entry) 
        {
            BusinessLayer db = new BusinessLayer();
            MerchantRegistration entity = entry.merchant;
            try
            {
                entity.RegistrationCode = Method.GetTransactionCode(db, SystemSetting.RegisMerchantCode);
                entity.AddressCode = String.Format("{0}{1}", SystemSetting.MerchantCode, entity.RegistrationCode.Substring(3, entity.RegistrationCode.Length - 3));
                entity.address.AddressCode = entity.AddressCode;
                entity.RegistrationStatus = RegistrationStatus.Request;
                entity.OwnerID = db.GetUserProfileID();
                entity.RegistrationDate = DateTime.Now;

                entity.address.CreatedBy = entity.CreatedBy = Membership.GetUser().UserName;
                entity.address.CreatedDate = entity.CreatedDate = DateTime.Now;
                db.merchantRegistrations.Add(entity);

                foreach (HttpPostedFileBase file in entry.files) 
                {
                    SampleProduct product = entry.sample.FirstOrDefault(x => x.filename == file.FileName);
                    if (Method.UploadFileValidation(file, 1024 * 100, new String[] { ".jpg" }))
                    {
                        String MerchantFolder = Path.Combine(Server.MapPath(SystemSetting.Default_Upload_Registration), entity.RegistrationCode);

                        if (!Directory.Exists(MerchantFolder)) Directory.CreateDirectory(MerchantFolder);

                        var path = Path.Combine(MerchantFolder, file.FileName);
                        file.SaveAs(path);

                        MerchantRegistrationDetail detail = new MerchantRegistrationDetail();
                        detail.ProductName = product.ProductName;
                        detail.Description = product.ProductDescription;
                        detail.ImgLink = String.Format(@"{0}{1}\{2}", SystemSetting.Default_Upload_Registration, entity.RegistrationCode, file.FileName);
                        detail.registration = entity;
                        detail.CreatedBy = Membership.GetUser().UserName;
                        detail.CreatedDate = DateTime.Now;
                        db.merchantRegistrationDetails.Add(detail);
                    }
                }

                db.SaveChanges();

                TempData["SaveResult"] = Json(new { ok = true, message = "Data berhasil disimpan" });
                return RedirectToAction("Merchant", entity);
            }
            catch (Exception ex)
            {
                TempData["SaveResult"] = Json(new { ok = false, message = "Data gagal disimpan : " + ex.Message });

                List<StandardCode> sc = db.standardcodes.Where(x => x.ParentID == SCConstant.Provinsi || x.ParentID == SCConstant.Negara).ToList();
                sc.Insert(0, new StandardCode { StandardCodeID = "", StandardCodeName = "" });
                ViewBag.Province = new SelectList(sc.Where(x => x.ParentID == SCConstant.Provinsi || x.StandardCodeID == ""), "StandardCodeID", "StandardCodeName", entity.address.Province);
                ViewBag.Country = new SelectList(sc.Where(x => x.ParentID == SCConstant.Negara || x.StandardCodeID == ""), "StandardCodeID", "StandardCodeName", entity.address.Country);
                Dictionary<String, String> DictProvince = new Dictionary<String, String>();
                foreach (StandardCode s in sc.Where(x => x.ParentID == SCConstant.Provinsi)) DictProvince.Add(s.StandardCodeName, s.Alias);
                ViewBag.provinces = Json(DictProvince);

                return View("Merchant");
            }
        }

        public ActionResult Agent() 
        {
            BusinessLayer db = new BusinessLayer();
            Int32 UserID = db.GetUserProfileID();
            Agent agent = db.agents.FirstOrDefault(x => x.UserID == UserID);
            if (agent != null)
            {
                String Level = agent.AgentType;
                List<StandardCode> lstStandardCode = db.standardcodes.Where(x => x.ParentID == SCConstant.Tipe_Agen && String.Compare(x.StandardCodeID, agent.AgentType) > 0).ToList();
                ViewBag.AgentType = new SelectList(lstStandardCode, "StandardCodeID", "StandardCodeName");
            }
            else 
            {
                List<StandardCode> lstStandardCode = db.standardcodes.Where(x => x.ParentID == SCConstant.Tipe_Agen).ToList();
                ViewBag.AgentType = new SelectList(lstStandardCode, "StandardCodeID", "StandardCodeName");
            }
            
            return View();
        }

        [HttpPost, ActionName("Agent")]
        public ActionResult AgentRegistration(String AgentType)
        {
            BusinessLayer db = new BusinessLayer();
            Int32 UserID = db.GetUserProfileID();
            try
            {
                
                AgentRegistration entity = new AgentRegistration();
                entity.RegistrationCode = Method.GetTransactionCode(db, SystemSetting.RegisAgenCode);
                entity.RegistrationDate = DateTime.Now;
                entity.AgentType = AgentType;
                entity.UserID = UserID;
                entity.RegistrationStatus = RegistrationStatus.Request;
                entity.IsDeleted = false;
                entity.CreatedBy = Membership.GetUser().UserName;
                entity.CreatedDate = DateTime.Now;
                db.agentRegistrations.Add(entity);
                db.SaveChanges();
                TempData["SaveResult"] = Json(new { ok = true, message = "Berhasil melakukan registrasi" });
                return RedirectToAction("Agent");
            }
            catch (Exception ex)
            {
                TempData["SaveResult"] = Json(new { ok = false, message = "Tidak dapat mendaftar : " + ex.Message });
                Agent agent = db.agents.FirstOrDefault(x => x.UserID == UserID);
                if (agent != null)
                {
                    String Level = agent.AgentType;
                    List<StandardCode> lstStandardCode = db.standardcodes.Where(x => x.ParentID == SCConstant.Tipe_Agen && String.Compare(x.StandardCodeID, agent.AgentType) > 0).ToList();
                    ViewBag.AgentType = new SelectList(lstStandardCode, "StandardCodeID", "StandardCodeName");
                }
                else
                {
                    List<StandardCode> lstStandardCode = db.standardcodes.Where(x => x.ParentID == SCConstant.Tipe_Agen).ToList();
                    ViewBag.AgentType = new SelectList(lstStandardCode, "StandardCodeID", "StandardCodeName");
                }
                return View();
            }
            
        }
    }
}
