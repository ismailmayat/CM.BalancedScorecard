using System;
using System.Collections.Generic;
using System.Web.Http;
using CM.BalancedScoreboard.Services.Abstract;
using CM.BalancedScoreboard.Services.Dto;
using CM.BalancedScoreboard.Services.ViewModel;

namespace CM.BalancedScoreboard.Web.Controllers
{
    public class IndicatorController : ApiController
    {
        private readonly IIndicatorService _service;

        public IndicatorController(IIndicatorService service)
        {
            _service = service;
        }

        // GET: api/Indicator
        public IEnumerable<IndicatorViewModel> Get(string filter)
        {
            return _service.GetIndicators(filter);
        }

        // GET: api/Indicator/5
        public IndicatorViewModel Get(Guid id)
        {
            return _service.GetIndicator(id);
        }

        // POST: api/Indicator
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Indicator/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Indicator/5
        public void Delete(int id)
        {
        }
    }
}
