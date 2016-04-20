using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Todoku.Models;
using System.Web.Security;

namespace Todoku.Areas.Merchants.Controllers
{
    public class CustomerOrderController : Controller
    {
        //
        // GET: /Merchants/CustomerOrder/

        public ActionResult Index(Int32 MerchantID)
        {
            BusinessLayer db = new BusinessLayer();
            String OwnerID = Membership.GetUser().ProviderUserKey.ToString();
            List<CustomerOrder> cos = db.customerOrder
                .Where(x => x.MerchantID == MerchantID && (x.RequestStatus == RequestStatus.Booked || x.RequestStatus == RequestStatus.ConfirmedByAdmin))
                .ToList();
            return PartialView(cos);
        }

        //
        // GET: /Merchants/CustomerOrder/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Merchants/CustomerOrder/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Merchants/CustomerOrder/Create

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
        // GET: /Merchants/CustomerOrder/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Merchants/CustomerOrder/Edit/5

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
        // GET: /Merchants/CustomerOrder/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Merchants/CustomerOrder/Delete/5

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
