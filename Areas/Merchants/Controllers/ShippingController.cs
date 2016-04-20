using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Todoku.Models;
using System.Web.Security;

namespace Todoku.Areas.Merchants.Controllers
{
    public class ShippingController : Controller
    {
        //
        // GET: /Merchants/Shipping/

        public ActionResult Index(Int32 MerchantID)
        {
            BusinessLayer db = new BusinessLayer();
            String OwnerID = Membership.GetUser().UserName;
            List<ItemDeliveryHd> itemdeliveryhds = db.itemdeliveryhds.Where(x => x.MerchantID == MerchantID && x.DeliveryStatus == DeliveryStatus.Prepared).ToList();
            return PartialView(itemdeliveryhds);
        }

        //
        // GET: /Merchants/Shipping/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Merchants/Shipping/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Merchants/Shipping/Create

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
        // GET: /Merchants/Shipping/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Merchants/Shipping/Edit/5

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
        // GET: /Merchants/Shipping/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Merchants/Shipping/Delete/5

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
