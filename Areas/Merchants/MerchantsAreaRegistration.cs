using System.Web.Mvc;

namespace Todoku.Areas.Merchants
{
    public class MerchantsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Merchants";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Merchants_detail",
                "Merchants/Detail/{id}",
                new { controller = "Home", action = "Detail", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Merchants_default",
                "Merchants/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
