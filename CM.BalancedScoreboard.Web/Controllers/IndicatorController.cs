using System;
using System.Collections.Generic;
using System.Web.Http;
using CM.BalancedScoreboard.Services.Abstract;
using System.Net.Http;
using System.Net;
using CM.BalancedScoreboard.Services.ViewModel.Indicators;

namespace CM.BalancedScoreboard.Web.Controllers
{
    public class IndicatorController : ApiController
    {
        private readonly IIndicatorService _service;

        public IndicatorController(IIndicatorService service)
        {
            _service = service;
        }

        public IndicatorListViewModel Get(string filter)
        {
            return _service.GetList(filter);
        }

        public HttpResponseMessage Get(Guid id)
        {
            try
            {
                var indicatorVm = _service.GetSingle(id);
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

        public HttpResponseMessage Post([FromBody]IndicatorDetailsViewModel indicatorVm)
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

        public HttpResponseMessage Put(Guid id, [FromBody]IndicatorDetailsViewModel indicatorVm)
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

        public HttpResponseMessage Delete(Guid id)
        {
            try
            {
                _service.Delete(id);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}
