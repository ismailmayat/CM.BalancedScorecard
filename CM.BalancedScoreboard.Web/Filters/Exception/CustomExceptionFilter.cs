using log4net;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace CM.BalancedScoreboard.Web.Filters.Exception
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        private ILog _logger = LogManager.GetLogger(typeof(CustomExceptionFilter));

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            _logger.Error(actionExecutedContext.Exception);
            actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }
    }
}