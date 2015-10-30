using AutoMapper.QueryableExtensions;
using CM.BalancedScoreboard.Data.Repository.Abstract;
using CM.BalancedScoreboard.Domain.Model.Projects;
using CM.BalancedScoreboard.Services.Abstract;
using CM.BalancedScoreboard.Services.ViewModel.Projects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CM.BalancedScoreboard.Services.Implementation
{
    public class ProjectService : IProjectsService
    {
        private readonly IProjectRepository _repository;

        public ProjectService(IProjectRepository repository)
        {
            _repository = repository;
        }

        public IList<ProjectViewModel> GetAll()
        {
            var projects = _repository.Get(x => x.Active);
            return projects.Project().To<ProjectViewModel>().ToList();
        }

        public void Add(ProjectViewModel projectVm)
        {
            var project = AutoMapper.Mapper.Map<Project>(projectVm);
            _repository.Add(project);
        }

        public void Update(ProjectViewModel projectVm)
        {
            var indicator = _repository.Single(i => i.Id == projectVm.Id);
            _repository.Update(AutoMapper.Mapper.Map(projectVm, indicator));
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }
    }
}
