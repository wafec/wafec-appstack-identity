using System.Web;
using System.Web.Mvc;

namespace Wafec.AppStack.Identity.App
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
