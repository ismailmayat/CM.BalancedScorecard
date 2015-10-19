using System;
using System.Collections.Generic;
using System.Web.Http;
using CM.BalancedScoreboard.Services.Abstract;
using CM.BalancedScoreboard.Services.ViewModel;
using System.Net.Http;
using System.Net;

namespace CM.BalancedScoreboard.Web.Controllers
{
    public class IndicatorController : ApiController
    {
        private readonly IIndicatorService _service;

        public IndicatorController(IIndicatorService service)
        {
            _service = service;
        }

        public IEnumerable<IndicatorViewModel> Get(string filter)
        {
            return _service.GetIndicators(filter);
        }

        public HttpResponseMessage Get(Guid id)
        {
            try
            {
                var indicatorVm = _service.GetIndicator(id);
                if (indicatorVm != null)
                    return Request.CreateResponse(HttpStatusCode.OK, indicatorVm);
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        public HttpResponseMessage Post([FromBody]IndicatorViewModel indicatorVm)
        {
            try
            {
                _service.Add(indicatorVm);
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        public HttpResponseMessage Put(Guid id, [FromBody]IndicatorViewModel indicatorVm)
        {
            try
            {
                _service.Update(id, indicatorVm);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        public void Delete(Guid id)
        {
        }
    }
}
