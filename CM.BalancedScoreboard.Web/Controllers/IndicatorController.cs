using System.Collections.Generic;
using System.Web.Http;
using CM.BalancedScoreboard.Services.Abstract;
using CM.BalancedScoreboard.Services.Dto;

namespace CM.BalancedScoreboard.Web.Controllers
{
    public class IndicatorController : ApiController
    {
        private readonly IIndicatorService _service;

        private IndicatorController(IIndicatorService service)
        {
            _service = service;
        }

        // GET: api/Indicator
        public IEnumerable<IndicatorDto> Get(string filter)
        {
            return _service.GetIndicators(filter);
        }

        // GET: api/Indicator/5
        public string Get(int id)
        {
            return "value";
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
