using CM.BalancedScorecard.Web.Filters.Exception;
using System.Web.Mvc;

namespace CM.BalancedScorecard.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
