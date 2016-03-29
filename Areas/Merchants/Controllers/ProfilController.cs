using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Todoku.Models;

namespace Todoku.Areas.Merchants.Controllers
{
    public class ProfilController : Controller
    {
        //
        // GET: /Merchants/Profil/

        public ActionResult Index()
        {
            if (TempData.Peek("MerchantID") != null)
            {
                BusinessLayer db = new BusinessLayer();
                Int32 MerchantID = Convert.ToInt32(TempData.Peek("MerchantID"));
                ViewBag.PartialView = "LeftPanel";
                ViewBag.filterExpression = "Merchant_Detail";
                Merchant entity = db.merchants.Find(MerchantID);
                List<StandardCode> sc = db.standardcodes.Where(x => x.ParentID == SCConstant.Provinsi || x.ParentID == SCConstant.Negara).ToList();
                sc.Insert(0, new StandardCode { StandardCodeID = "", StandardCodeName = "" });
                ViewBag.Province = new SelectList(sc.Where(x => x.ParentID == SCConstant.Provinsi || x.StandardCodeID == ""), "StandardCodeID", "StandardCodeName", entity.address.Province);
                ViewBag.Country = new SelectList(sc.Where(x => x.ParentID == SCConstant.Negara || x.StandardCodeID == ""), "StandardCodeID", "StandardCodeName", entity.address.Country);
                return View(entity);
            }
            else 
            {
                return RedirectToAction("","");
            }
        }

        //
        // GET: /Merchants/Profil/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Merchants/Profil/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Merchants/Profil/Create

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
        // GET: /Merchants/Profil/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Merchants/Profil/Edit/5

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
        // GET: /Merchants/Profil/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Merchants/Profil/Delete/5

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
    }
}
