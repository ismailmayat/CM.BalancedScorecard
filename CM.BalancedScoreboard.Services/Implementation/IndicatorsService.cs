using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using CM.BalancedScoreboard.Data.Repository.Abstract;
using CM.BalancedScoreboard.Domain.Abstract.Indicators;
using CM.BalancedScoreboard.Domain.Model.Indicators;
using CM.BalancedScoreboard.Services.Abstract;
using CM.BalancedScoreboard.Services.ViewModel.Indicators;

namespace CM.BalancedScoreboard.Services.Implementation
{
    public class IndicatorsService : IIndicatorsService
    {
        private readonly IIndicatorsRepository _repository;
        private readonly IIndicatorStateCalculator _stateCalculator;

        public IndicatorsService(IIndicatorsRepository repository, IIndicatorStateCalculator stateCalculator)
        {
            _repository = repository;
            _stateCalculator = stateCalculator;
        }

        public IEnumerable<IndicatorViewModel> GetIndicators(string filter)
        {
            var indicators =
                _repository.Get(
                    i => i.Name.Contains(filter) || i.Code.Contains(filter) || i.Description.Contains(filter) ||
                         (i.Manager.Firstname + i.Manager.Surname).Contains(filter));

            var indicatorVms = indicators.Project().To<IndicatorViewModel>().ToList();
            foreach (var indicatorVm in indicatorVms)
            {
                indicatorVm.State = _stateCalculator.Calculate(indicatorVm.LastMeasureDate, indicatorVm.LastRealValue,
                indicatorVm.LastTargetValue, indicatorVm.PeriodicityType, indicatorVm.ComparisonValueType,
                indicatorVm.ObjectValueType, indicatorVm.FulfillmentRate);
            }

            return indicatorVms;
        }

        public IndicatorDetailsViewModel GetIndicator(Guid id)
        {
            var indicator = _repository.Single(i => i.Id == id, i => i.Measures);

            var indicatorVm = AutoMapper.Mapper.Map<IndicatorViewModel>(indicator);
            indicatorVm.State = _stateCalculator.Calculate(indicatorVm.LastMeasureDate, indicatorVm.LastRealValue,
                indicatorVm.LastTargetValue, indicatorVm.PeriodicityType, indicatorVm.ComparisonValueType,
                indicatorVm.ObjectValueType, indicatorVm.FulfillmentRate);

            return new IndicatorDetailsViewModel()
            {
                Indicator = indicatorVm
            };
        }

        public void Add(IndicatorViewModel indicatorVm)
        {
            var indicator = AutoMapper.Mapper.Map<Indicator>(indicatorVm);
            _repository.Add(indicator);
        }

        public void Update(IndicatorViewModel indicatorVm)
        {
            var indicator = _repository.Single(i => i.Id == indicatorVm.Id);
            _repository.Update(AutoMapper.Mapper.Map(indicatorVm, indicator));
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }

        public IEnumerable<IndicatorMeasureListViewModel> GetMeasures(Guid indicatorId)
        {
            var indicator = _repository.Single(i => i.Id == indicatorId, i => i.Measures);

            return indicator?.Measures.GroupBy(i => i.Date.Year)
                .Select(gb => new IndicatorMeasureListViewModel()
                {
                    Year = gb.Key,
                    Measures = AutoMapper.Mapper.Map<List<IndicatorMeasureViewModel>>(gb)
                }).OrderByDescending(im => im.Year);
        }

        public bool AddMeasure(IndicatorMeasureViewModel indicatorMeasureVm)
        {
            var indicatorMeasure = AutoMapper.Mapper.Map<IndicatorMeasure>(indicatorMeasureVm);
            indicatorMeasure.Id = Guid.NewGuid();
            return _repository.AddMeasure(indicatorMeasure);
        }

        public bool UpdateMeasure(IndicatorMeasureViewModel indicatorMeasureVm)
        {
            return _repository.UpdateMeasure(AutoMapper.Mapper.Map<IndicatorMeasure>(indicatorMeasureVm));
        }

        public bool DeleteMeasure(Guid indicatorId, Guid indicatorMeasureId)
        {
            return _repository.DeleteMeasure(indicatorId, indicatorMeasureId);
        }
    }
}
