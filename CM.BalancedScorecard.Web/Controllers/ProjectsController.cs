using CM.BalancedScorecard.Services.Abstract;
using CM.BalancedScorecard.Services.ViewModel.Projects;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CM.BalancedScorecard.Web.Controllers
{
    public class ProjectsController : ApiController
    {
        private readonly IProjectsService _service;

        public ProjectsController(IProjectsService service)
        {
            _service = service;
        }

        public IList<ProjectViewModel> Get(string filter)
        {
            return _service.GetAll();
        }

        public HttpResponseMessage Post([FromBody]ProjectViewModel projectVm)
        {
            try
            {
                _service.Add(projectVm);
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                //log error
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}
