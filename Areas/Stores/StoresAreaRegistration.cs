using System.Web.Mvc;

namespace Todoku.Areas.Stores
{
    public class StoresAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Stores";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Stores_product",
                "Stores/Detail/{id}",
                new { Controller = "Product", action = "Detail", id = UrlParameter.Optional }
                );

            context.MapRoute(
                "Stores_default",
                "Stores/{controller}/{action}/{id}",
                new { controller = "Stores", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
