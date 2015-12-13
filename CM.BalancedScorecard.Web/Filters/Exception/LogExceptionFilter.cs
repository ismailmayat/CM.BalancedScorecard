using log4net;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace CM.BalancedScorecard.Web.Filters.Exception
{
    public class LogExceptionFilter : ExceptionFilterAttribute
    {
        private ILog _logger = LogManager.GetLogger(typeof(LogExceptionFilter));

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            _logger.Error(actionExecutedContext.Exception);
            actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }
    }
}