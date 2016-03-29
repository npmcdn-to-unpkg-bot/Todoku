using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Todoku.Models;

namespace Todoku.Areas.Members.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Regisration/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Regisration/Create

        public ActionResult Register()
        {
            BusinessLayer db = new BusinessLayer();
            List<StandardCode> sc = db.standardcodes.Where(x => x.ParentID == SCConstant.Jenis_Kelamin ||
                x.ParentID == SCConstant.Provinsi ||
                x.ParentID == SCConstant.Negara).ToList();
            ViewBag.Gender = new SelectList(sc.Where(x => x.ParentID == SCConstant.Jenis_Kelamin), "StandardCodeID", "StandardCodeName");
            ViewBag.Province = new SelectList(sc.Where(x => x.ParentID == SCConstant.Provinsi), "StandardCodeID", "StandardCodeName");
            ViewBag.Country = new SelectList(sc.Where(x => x.ParentID == SCConstant.Negara), "StandardCodeID", "StandardCodeName");
            return View();
        } 

        //
        // POST: /Regisration/Create

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            try
            {
                // TODO: Add insert logic here

                if (ModelState.IsValid)
                {
                    // Attempt to register the user
                    MembershipCreateStatus createStatus;
                    Membership.CreateUser(model.UserName, model.Password, model.Email, model.PasswordQuestion, model.PasswordAnswer, true, null, out createStatus);
                    
                    if (createStatus == MembershipCreateStatus.Success)
                    {
                        Roles.AddUserToRole(model.UserName, "Member");
                        UserProfile up = new UserProfile();
                        try
                        {
                            BusinessLayer db = new BusinessLayer();
                            up.Fullname = model.userprofile.Fullname;
                            up.UserName = model.UserName;
                            up.Gender = model.userprofile.Gender;
                            up.DateOfBirth = model.userprofile.DateOfBirth;
                            up.address = new Addresses();
                            String Code = String.Format("{0}{1}{2}{3}", SystemSetting.MemberCode, DateTime.Now.Year, DateTime.Now.Month.ToString("00"), DateTime.Now.Day.ToString("00"));
                            int count = db.addresses.Where(x => x.AddressCode.Contains(Code)).Count() + 1;
                            up.AddressCode = up.address.AddressCode = String.Format("{0}{1}",Code, count.ToString("00000"));
                            up.address.Phone = model.userprofile.address.Phone;
                            up.address.Address = model.userprofile.address.Address;
                            up.address.City = model.userprofile.address.City;
                            up.address.Province = model.userprofile.address.Province;
                            up.address.Country = model.userprofile.address.Country;
                            db.userprofiles.Add(up);
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            String errMessage = ex.Message;
                            ModelState.AddModelError("", errMessage);
                            Membership.DeleteUser(model.UserName);
                            return View(up);
                        }
                        
                        FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                        return MigrateCarts(model.UserName, RedirectToAction("Index", "Home", new { area = "" }));
                    }
                    else
                    {
                        ModelState.AddModelError("", ErrorCodeToString(createStatus));
                        return View();
                    }
                }
                return RedirectToAction("Register");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Regisration/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Regisration/Edit/5

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
        // GET: /Regisration/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Regisration/Delete/5

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

        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MembershipUser user = Membership.GetUser(model.UserName);
                    if (user != null && user.IsLockedOut)
                    {
                        if (DateTime.Compare(user.LastLockoutDate.AddMinutes(5), DateTime.Now) < 0)
                        {
                            user.UnlockUser();
                        }
                    }

                    if (user != null && !user.IsLockedOut && Membership.ValidateUser(model.UserName, model.Password))
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                            && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            //return Redirect(returnUrl);
                            return MigrateCarts(model.UserName, Redirect(returnUrl));
                        }
                        else
                        {
                            //return RedirectToAction("Index", "Home", new { area = "" });
                            return MigrateCarts(model.UserName, RedirectToAction("Index", "Home", new { area = "" }));
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "The user name or password provided is incorrect." + Membership.ValidateUser(model.UserName, model.Password));
                    }
                }
                catch (Exception ex) 
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
                
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult MigrateCarts(String UserName, ActionResult ar)
        {
            try
            {
                BusinessLayer db = new BusinessLayer();
                UserProfile up = db.userprofiles.FirstOrDefault(x => x.UserName == UserName); 
                List<Cart> carts = AppSession.GetCartUsingCookie(this.HttpContext);
                foreach (Cart cart in carts)
                {
                    Cart entity = new Cart();
                    entity.ProductID = cart.ProductID;
                    entity.Quantity = cart.Quantity;
                    entity.DiscountAmount = cart.DiscountAmount;
                    entity.DiscountInPercentage = cart.DiscountInPercentage;
                    entity.LineAmount = cart.LineAmount;
                    entity.CreatedDate = cart.CreatedDate;
                    entity.UserName = UserName;
                    entity.ItemStatus = ItemStatus.Requested;
                    db.carts.Add(entity);
                }

                AppSession.SetCartUsingCookie(this.HttpContext,new List<Cart>());
                db.SaveChanges();
                return ar;
            }
            catch
            {
                throw new Exception();
            }
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
