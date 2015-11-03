using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using CM.BalancedScoreboard.Data.Repository.Abstract;
using CM.BalancedScoreboard.Domain.Model.Indicators;
using CM.BalancedScoreboard.Services.Abstract;
using CM.BalancedScoreboard.Services.ViewModel.Indicators;

namespace CM.BalancedScoreboard.Services.Implementation
{
    public class IndicatorsService : IIndicatorsService
    {
        private readonly IIndicatorRepository _repository;

        public IndicatorsService(IIndicatorRepository repository)
        {
            _repository = repository;
        }

        public List<IndicatorViewModel> GetIndicators(string filter)
        {
            var indicators =
                _repository.Get(
                    i => i.Name.Contains(filter) || i.Code.Contains(filter) || i.Description.Contains(filter) ||
                         (i.Manager.Firstname + i.Manager.Surname).Contains(filter));

            return indicators.Project().To<IndicatorViewModel>().ToList();
        }

        public IndicatorDetailsViewModel GetIndicator(Guid id)
        {
            var indicator = _repository.Single(i => i.Id == id, i => i.Measures);

            return new IndicatorDetailsViewModel()
            {
                Indicator = AutoMapper.Mapper.Map<IndicatorViewModel>(indicator)
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

        public Dictionary<int, List<IndicatorMeasureViewModel>> GetMeasures(Guid indicatorId)
        {
            var indicator = _repository.Single(i => i.Id == indicatorId, i => i.Measures);

            return indicator?.Measures.GroupBy(i => i.Date.Year)
                .ToDictionary(gb => gb.Key, AutoMapper.Mapper.Map<List<IndicatorMeasureViewModel>>);
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
