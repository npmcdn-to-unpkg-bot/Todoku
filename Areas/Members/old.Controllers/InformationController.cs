using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Todoku.Models;

namespace Todoku.Areas.Members.Controllers
{
    public class old_InformationController : Controller
    {
        //
        // GET: /Members/Information/

        public ActionResult Index()
        {
            BusinessLayer db = new BusinessLayer();
            Int32 userid = db.GetUserProfileID();
            List<PurchaseOrderHd> list = db.purchaseorderhds.Where(x => x.OrderStatus != OrderStatus.Void && x.CustomerID == userid ).ToList();
            List<ItemDeliveryHd> lstid = db.itemdeliveryhds.Where(x => x.DeliveryStatus != DeliveryStatus.Void && x.CustomerID == userid).ToList();
            List<PurchaseReceiveHd> lstPrhd = db.purchasereceivehds.Where(x => x.ReceiveStatus != ReceiveStatus.Closed && x.CustomerID == userid).ToList();
            
            PurchaseOrderInformation poi = new PurchaseOrderInformation();
            poi.lstPurchaseOrder = list;
            poi.lstItemDelivery = lstid;
            poi.lstPurchaseReceive = lstPrhd;
            
            return View(poi);
        }
    }
}
