using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Todoku.Models;
using System.Data;
using System.Web.Security;

namespace Todoku.Areas.Admin.Controllers
{
    [Authorize]
    public class RegistrationController : Controller
    {
        //
        // GET: /Admin/Registration/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MerchantRegistration(Int32 Page = 1, Int32 Rows = SystemSetting.Default_Grid_Row) 
        {
            BusinessLayer db = new BusinessLayer();
            List<MerchantRegistration> merchants = db.merchantRegistrations.Where(x => x.RegistrationStatus == RegistrationStatus.Request).ToList();
            Int32 Pages = 0;
            merchants = Method.SetPagination(merchants, Rows, Page, ref Pages);
            ViewBag.Page = Page;
            ViewBag.Pages = Pages;
            return View(merchants);
        }

        [HttpPost, ActionName("MerchantRegistration")]
        public ActionResult MerchantConfirmation(MerchantRegistration entity) 
        {
            try
            {
                BusinessLayer db = new BusinessLayer();
                MerchantRegistration obj = db.merchantRegistrations.FirstOrDefault(x => x.RegistrationID == entity.RegistrationID);
                obj.RegistrationStatus = RegistrationStatus.ConfirmedByAdmin;
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
                TempData["SaveResult"] = Json(new { ok = true, message = "Data berhasil dikonfirmasi" });
                return Json(new { ok = true });
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, message = "Data tidak dapat dikonfirmasi : " + ex.Message });
            }
        }

        public ActionResult AgentRegistration(Int32 Page = 1, Int32 Rows = SystemSetting.Default_Grid_Row) 
        {
            BusinessLayer db = new BusinessLayer();
            List<AgentRegistration> agents = db.agentRegistrations.Where(x => x.RegistrationStatus == RegistrationStatus.Request).ToList();
            Int32 Pages = 0;
            agents = Method.SetPagination(agents, Rows, Page, ref Pages);
            ViewBag.Page = Page;
            ViewBag.Pages = Pages;
            return View(agents);
        }

        [HttpPost, ActionName("AgentRegistration")]
        public ActionResult AgentConfirmation(AgentRegistration entity)
        {
            try
            {
                BusinessLayer db = new BusinessLayer();
                AgentRegistration obj = db.agentRegistrations.FirstOrDefault(x => x.RegistrationID == entity.RegistrationID);
                obj.RegistrationStatus = RegistrationStatus.ConfirmedByAdmin;
                db.Entry(obj).State = EntityState.Modified;

                Agent agent = db.agents.FirstOrDefault(x => x.UserID == obj.UserID);
                if (agent == null)
                {
                    agent = new Agent();
                    UserProfile profile = db.userprofiles.FirstOrDefault(x => x.UserProfileID == obj.UserID && !x.IsDeleted);
                    agent.AgentCode = String.Format("{0}{1}", SystemSetting.AgentCode, obj.RegistrationCode.Substring(3, obj.RegistrationCode.Length - 3));
                    agent.AddressCode = profile.AddressCode;
                    agent.UserID = obj.UserID;
                    agent.AgentType = obj.AgentType;
                    agent.JoinDate = DateTime.Now;
                    agent.IsActive = true;
                    agent.IsDeleted = false;
                    agent.CreatedBy = Membership.GetUser().UserName;
                    agent.CreatedDate = DateTime.Now;
                    db.agents.Add(agent);
                    
                    String[] lstRole = Roles.GetRolesForUser(profile.UserName);
                    if (!lstRole.Contains(UserRole.Agent))
                    {
                        Roles.AddUserToRole(profile.UserName, UserRole.Agent);
                    }
                    
                    db.SaveChanges();
                }
                else 
                {
                    agent.AgentType = obj.AgentType;
                    agent.LastUpdatedBy = Membership.GetUser().UserName;
                    agent.LastUpdatedDate = DateTime.Now;
                    db.Entry(agent).State = EntityState.Modified;
                    db.SaveChanges();
                }

                TempData["SaveResult"] = Json(new { ok = true, message = "Data berhasil dikonfirmasi" });
                return Json(new { ok = true });
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, message = "Data tidak dapat dikonfirmasi : " + ex.Message });
            }
        }
    }
}
