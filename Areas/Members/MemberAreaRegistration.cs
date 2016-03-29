using System.Web.Mvc;

namespace Todoku.Areas.Members
{
    public class MemberAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Members";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Members_LogOn",
                "LogOn/",
                new { controller = "Account", action = "LogOn", area = "Members" });
            
            context.MapRoute(
                "Members_registration",
                "Registration/",
                new { controller = "Account", action = "Register", area = "Members" });
            
            context.MapRoute(
                "Members_cart",
                "Cart/",
                new { controller = "Cart", action = "Index", area = "Members" });

            context.MapRoute(
                "Members_default",
                "Members/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
