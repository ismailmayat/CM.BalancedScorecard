using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using CM.BalancedScoreboard.Data.Repository.Abstract;
using CM.BalancedScoreboard.Domain.Model.Indicators;
using CM.BalancedScoreboard.Services.Abstract;
using CM.BalancedScoreboard.Services.ViewModel;

namespace CM.BalancedScoreboard.Services.Implementation
{
    public class IndicatorService : IIndicatorService
    {
        private readonly IBaseRepository<Indicator> _repository;

        public IndicatorService(IBaseRepository<Indicator> repository)
        {
            _repository = repository;
        }

        public IEnumerable<IndicatorViewModel> GetIndicators(string filter)
        {
            var indicators =
                _repository.Get(
                    i => i.Name.Contains(filter) || i.Code.Contains(filter) || i.Description.Contains(filter) ||
                         (i.Manager.Firstname + i.Manager.Surname).Contains(filter));

            return indicators.Project().To<IndicatorViewModel>().ToList();
        }

        public IndicatorViewModel GetIndicator(Guid id)
        {
            var indicator = _repository.Single(i => i.Id == id, i => i.Values);

            return AutoMapper.Mapper.Map<IndicatorViewModel>(indicator);
        }
    }
}
