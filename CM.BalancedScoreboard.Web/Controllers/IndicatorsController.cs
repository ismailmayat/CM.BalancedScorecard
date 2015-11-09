using CM.BalancedScoreboard.Services.Abstract.Indicators;
using CM.BalancedScoreboard.Services.ViewModel.Indicators;
using System;
using System.Web.Http;

namespace CM.BalancedScoreboard.Web.Controllers
{
    public class IndicatorsController : ApiController
    {
        private readonly IIndicatorsService _service;

        public IndicatorsController(IIndicatorsService service)
        {
            _service = service;
        }

        public IHttpActionResult Get(string filter)
        {
            try
            {
                return Ok(_service.GetIndicators(filter));
            }
            catch (Exception ex)
            {
                //log error
                return InternalServerError();
            }
        }

        public IHttpActionResult Get(Guid id)
        {
            try
            {
                var indicatorVm = _service.GetIndicator(id);
                if (indicatorVm != null)
                    return Ok(indicatorVm);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                //log error
                return InternalServerError();
            }
        }

        public IHttpActionResult Post([FromBody]IndicatorViewModel indicatorVm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _service.Add(indicatorVm);
                    return Ok();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                //log error
                return InternalServerError();
            }
        }

        public IHttpActionResult Put(Guid id, [FromBody]IndicatorViewModel indicatorVm)
        {
            try
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
            catch (Exception ex)
            {
                //log error
                return InternalServerError();
            }
        }

        public IHttpActionResult Delete(Guid id)
        {
            try
            {
                _service.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [Route("api/indicators/{id}/measures")]
        public IHttpActionResult GetMeasures(Guid id)
        {
            try
            {
                var indicatorMeasures = _service.GetMeasures(id);
                if (indicatorMeasures != null)
                    return Ok(indicatorMeasures);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                //log error
                return InternalServerError();
            }
        }

        [Route("api/indicators/{id}/measures")]
        [HttpPost]
        public IHttpActionResult Post(Guid id, [FromBody] IndicatorMeasureViewModel indicatorMeasureVm)
        {
            try
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
            catch (Exception ex)
            {
                //log error
                return InternalServerError();
            }
        }

        [Route("api/indicators/{id}/measures/{measureId}")]
        [HttpPut]
        public IHttpActionResult Put(Guid id, Guid measureId, [FromBody]IndicatorMeasureViewModel indicatorMeasureVm)
        {
            try
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
            catch (Exception ex)
            {
                //log error
                return InternalServerError();
            }
        }

        [Route("api/indicators/{id}/measures/{measureId}")]
        [HttpDelete]
        public IHttpActionResult Delete(Guid id, Guid measureId)
        {
            try
            {
                if (_service.DeleteMeasure(id, measureId))
                    return Ok();
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                //log error
                return InternalServerError();
            }
        }
    }
}
