using CM.BalancedScoreboard.Data.Repository.Abstract;
using CM.BalancedScoreboard.Domain.Abstract.Indicators;
using CM.BalancedScoreboard.Domain.Model.Indicators;
using CM.BalancedScoreboard.Services.Abstract.Indicators;
using CM.BalancedScoreboard.Services.ViewModel.Indicators;
using System;

namespace CM.BalancedScoreboard.Services.Implementation.Indicators
{
    public class IndicatorsService : IIndicatorsService
    {
        readonly IIndicatorsRepository _repository;
        readonly IIndicatorStateCalculator _stateCalculator;
        readonly IIndicatorViewModelFactory _viewModelFactory;

        public IndicatorsService(IIndicatorsRepository repository, IIndicatorStateCalculator stateCalculator, IIndicatorViewModelFactory viewModelFactory)
        {
            _repository = repository;
            _stateCalculator = stateCalculator;
            _viewModelFactory = viewModelFactory;
        }

        public IndicatorListViewModel GetIndicators(string filter)
        {
            var indicators =
                _repository.Get(
                    i => i.Name.Contains(filter) || i.Code.Contains(filter) || i.Description.Contains(filter) ||
                         (i.Manager.Firstname + i.Manager.Surname).Contains(filter));

            return _viewModelFactory.CreateIndicatorListViewModel(indicators);
        }

        public IndicatorDetailsViewModel GetIndicator(Guid id)
        {
            var indicator = _repository.Single(i => i.Id == id, i => i.Measures);
            return _viewModelFactory.CreateIndicatorDetailsViewModel(indicator);
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

        public IndicatorMeasureDetailsViewModel GetMeasures(Guid indicatorId)
        {
            var indicator = _repository.Single(i => i.Id == indicatorId, i => i.Measures);
            return _viewModelFactory.CreateMeasureDetailsViewModel(indicator);
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
