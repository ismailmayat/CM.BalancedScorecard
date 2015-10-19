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
        private readonly IIndicatorRepository _repository;

        public IndicatorService(IIndicatorRepository repository)
        {
            _repository = repository;
        }

        public IList<IndicatorViewModel> GetIndicators(string filter)
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

        public void Add(IndicatorViewModel indicatorVm)
        {
            var indicator = AutoMapper.Mapper.Map<Indicator>(indicatorVm);

            _repository.Add(indicator);
        }

        public void Update(Guid id, IndicatorViewModel indicatorVm)
        {
            var indicator = _repository.Single(i => i.Id == id);
            indicator = AutoMapper.Mapper.Map(indicatorVm, indicator);

            _repository.Update(indicator);
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }
    }
}
