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

        public IndicatorViewModel Get(Guid id)
        {
            return _service.GetIndicator(id);
        }

        public HttpResponseMessage Post([FromBody]IndicatorViewModel indicatorVm)
        {
            try
            {
                _service.Update(indicatorVm);
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        public HttpResponseMessage Put([FromBody]IndicatorViewModel indicatorVm)
        {
            try
            {
                _service.Update(indicatorVm);
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        public void Delete(int id)
        {
        }
    }
}
