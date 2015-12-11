using CM.BalancedScoreboard.Services.Abstract.Indicators;
using CM.BalancedScoreboard.Services.ViewModel.Indicators;
using System;
using System.Web.Http;

namespace CM.BalancedScoreboard.Web.Controllers
{
    public class IndicatorsController : ApiController
    {
        readonly IIndicatorsService _service;
        
        public IndicatorsController(IIndicatorsService service)
        {
            _service = service;
        }

        public IHttpActionResult Get(string filter)
        {
            return Ok(_service.GetIndicators(filter));
        }

        public IHttpActionResult Get(Guid id)
        {
            var indicatorVm = _service.GetIndicator(id);
            if (indicatorVm != null)
                return Ok(indicatorVm);
            else
                return BadRequest();
        }

        public IHttpActionResult Get()
        {
            var indicatorVm = _service.GetIndicator(null);
            if (indicatorVm != null)
                return Ok(indicatorVm);
            else
                return BadRequest();
        }

        public IHttpActionResult Post([FromBody]IndicatorViewModel indicatorVm)
        {
            if (ModelState.IsValid)
            {
                var id = _service.Add(indicatorVm);
                indicatorVm.Id = id;
                return Created("/Indicators/Details/" + id.ToString(), indicatorVm);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        public IHttpActionResult Put(Guid id, [FromBody]IndicatorViewModel indicatorVm)
        {
            indicatorVm.Id = id;
            if (ModelState.IsValid)
            {
                _service.Update(indicatorVm);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        public IHttpActionResult Delete(Guid id)
        {
            _service.Delete(id);
            return Ok();
        }

        [Route("api/indicators/{id}/measures")]
        public IHttpActionResult GetMeasures(Guid id)
        {
            var indicatorMeasures = _service.GetMeasures(id);
            if (indicatorMeasures != null)
                return Ok(indicatorMeasures);
            else
                return BadRequest();
        }

        [Route("api/indicators/{id}/measures")]
        [HttpPost]
        public IHttpActionResult Post(Guid id, [FromBody] IndicatorMeasureViewModel indicatorMeasureVm)
        {
            indicatorMeasureVm.IndicatorId = id;
            if (ModelState.IsValid)
            {
                if (_service.AddMeasure(indicatorMeasureVm))
                    return Ok();
                else
                    return BadRequest();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [Route("api/indicators/{id}/measures/{measureId}")]
        [HttpPut]
        public IHttpActionResult Put(Guid id, Guid measureId, [FromBody]IndicatorMeasureViewModel indicatorMeasureVm)
        {
            indicatorMeasureVm.IndicatorId = id;
            indicatorMeasureVm.Id = measureId;
            if (ModelState.IsValid)
            {
                if (_service.UpdateMeasure(indicatorMeasureVm))
                    return Ok();
                else
                    return BadRequest();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [Route("api/indicators/{id}/measures/{measureId}")]
        [HttpDelete]
        public IHttpActionResult Delete(Guid id, Guid measureId)
        {
            if (_service.DeleteMeasure(id, measureId))
                return Ok();
            else
                return BadRequest();
        }

        [Route("api/indicators/resources")]
        public IHttpActionResult GetResources()
        {
            return Ok(_service.GetResources());
        }
    }
}
