
using System.Web.Mvc;

namespace GestionAbscences.Areas.AdminN2
{
    public class AdminN2AreaRegistration : AreaRegistration
    {
        public override string AreaName {
            get {
                return "AdminN2";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "AdminN2_default",
                "AdminN2/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}