using CM.BalancedScoreboard.Web.Filters.Exception;
using System.Web.Mvc;

namespace CM.BalancedScoreboard.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
