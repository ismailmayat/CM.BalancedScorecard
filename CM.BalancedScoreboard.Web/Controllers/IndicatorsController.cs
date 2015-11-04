using System;
using System.Collections.Generic;
using System.Web.Http;
using CM.BalancedScoreboard.Services.Abstract;
using System.Net.Http;
using System.Net;
using CM.BalancedScoreboard.Services.ViewModel.Indicators;

namespace CM.BalancedScoreboard.Web.Controllers
{
    public class IndicatorsController : ApiController
    {
        private readonly IIndicatorsService _service;

        public IndicatorsController(IIndicatorsService service)
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
                //log error
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
                //log error
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        public HttpResponseMessage Put(Guid id, [FromBody]IndicatorViewModel indicatorVm)
        {
            try
            {
                indicatorVm.Id = id;
                _service.Update(indicatorVm);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                //log error
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

        [Route("api/indicators/{id}/measures")]
        public HttpResponseMessage GetMeasures(Guid id)
        {
            try
            {
                var indicatorMeasures = _service.GetMeasures(id);
                if (indicatorMeasures != null)
                    return Request.CreateResponse(HttpStatusCode.OK, indicatorMeasures);
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                //log error
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [Route("api/indicators/{id}/measures")]
        [HttpPost]
        public HttpResponseMessage Post(Guid id, [FromBody] IndicatorMeasureViewModel indicatorMeasureVm)
        {
            try
            {
                indicatorMeasureVm.IndicatorId = id;
                if (_service.AddMeasure(indicatorMeasureVm))
                    return new HttpResponseMessage(HttpStatusCode.Created);
                else
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                //log error
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        [Route("api/indicators/{id}/measures/{measureId}")]
        [HttpPut]
        public HttpResponseMessage Put(Guid id, Guid measureId, [FromBody]IndicatorMeasureViewModel indicatorMeasureVm)
        {
            try
            {
                indicatorMeasureVm.Id = measureId;
                if (_service.UpdateMeasure(indicatorMeasureVm))
                    return new HttpResponseMessage(HttpStatusCode.OK);
                else
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                //log error
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        [Route("api/indicators/{id}/measures/{measureId}")]
        [HttpDelete]
        public HttpResponseMessage Delete(Guid id, Guid measureId)
        {
            try
            {
                if (_service.DeleteMeasure(id, measureId))
                return new HttpResponseMessage(HttpStatusCode.OK);
                else
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                //log error
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}
