using CM.BalancedScoreboard.Services.ViewModel.Projects;
using System;
using System.Collections.Generic;

namespace CM.BalancedScoreboard.Services.Abstract
{
    public interface IProjectsService
    {
        IList<ProjectViewModel> GetAll();

        void Add(ProjectViewModel projectVm);

        void Update(ProjectViewModel projectVm);

        void Delete(Guid id);
    }
}
