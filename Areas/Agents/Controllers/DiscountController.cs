using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Todoku.Models;
using System.Web.Security;

namespace Todoku.Areas.Agents.Controllers
{
    [Authorize]
    public class DiscountController : Controller
    {
        //
        // GET: /Agents/Discount/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        public JsonResult Save(ProductSettingEntry data)
        {
            try
            {
                BusinessLayer db = new BusinessLayer();
                AgentDiscountSetting entity = new AgentDiscountSetting();
                entity.AgentID = db.GetAgentID();
                if(!data.IsForAll) entity.ProductID = data.ProductID;
                entity.DiscountAmount = data.DiscountAmount;
                entity.CreatedBy = Membership.GetUser().UserName;
                entity.CreatedDate = DateTime.Now;
                db.agentDiscountSettings.Add(entity);
                db.SaveChanges();

                return Json(new { ok = true, message = "Data berhasil disimpan" });
            }
            catch (Exception ex) 
            {
                return Json(new { ok = false, message = "Data gagal disimpan : "+ ex.Message });
            }
        }

        #region Ajax
        public JsonResult GetAgentDiscountList() 
        {
            BusinessLayer db = new BusinessLayer();
            Int32 AgentID = db.GetAgentID();
            List<AgentDiscountSetting> list = db.agentDiscountSettings.Where(x => !x.IsDeleted && x.AgentID == AgentID).ToList();
            return new JsonNetResult(list, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
