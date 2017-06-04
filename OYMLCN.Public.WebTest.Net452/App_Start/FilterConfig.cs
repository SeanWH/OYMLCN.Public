using System.Web;
using System.Web.Mvc;

namespace OYMLCN.Open.WebTest.Net452
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
