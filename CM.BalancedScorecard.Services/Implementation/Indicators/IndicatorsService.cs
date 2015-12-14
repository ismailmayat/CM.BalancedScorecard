﻿using CM.BalancedScorecard.Domain.Model.Indicators;
using CM.BalancedScorecard.Resources;
using CM.BalancedScorecard.Resources.Abstract;
using CM.BalancedScorecard.Services.Abstract.Indicators;
using CM.BalancedScorecard.Services.ViewModel.Indicators;
using System;
using System.Collections.Generic;
using System.Linq;
using CM.BalancedScorecard.Data.Repository.Abstract;
using CM.BalancedScorecard.Data.Repository.Abstract.Indicators;
using CM.BalancedScorecard.Domain.Model.Users;

namespace CM.BalancedScorecard.Services.Implementation.Indicators
{
    public class IndicatorsService : IIndicatorsService
    {
        private readonly IIndicatorsRepository _indicatorsRepository;
        private readonly IBaseRepository<IndicatorType> _indicatorTypeRepository;
        private readonly IBaseRepository<User> _usersRepository;
        private readonly IIndicatorViewModelFactory _viewModelFactory;
        private readonly IResourceManager _resourceManager;

        public IndicatorsService(IIndicatorsRepository indicatorsRepository, IBaseRepository<IndicatorType> indicatorTypeRepository, IBaseRepository<User> userRepository, IIndicatorViewModelFactory viewModelFactory, IResourceFactory resourceFactory)
        {
            _indicatorsRepository = indicatorsRepository;
            _indicatorTypeRepository = indicatorTypeRepository;
            _usersRepository = userRepository;
            _viewModelFactory = viewModelFactory;
            _resourceManager = resourceFactory.GetResourceManager(ResourceType.Indicators);
        }

        public IndicatorListViewModel GetIndicators(string filter)
        {
            var indicators = ApplyIndicatorsFilter(_indicatorsRepository.GetAll(), filter).OrderBy(i => i.Name);
            return _viewModelFactory.CreateIndicatorListViewModel(indicators);
        }

        public IndicatorDetailsViewModel GetIndicator(Guid? id)
        {
            var indicator = id.HasValue ? _indicatorsRepository.Single(i => i.Id == id, i => i.Measures) : new Indicator();
            var indicatorTypes = _indicatorTypeRepository.GetAll();
            var users = _usersRepository.GetAll();

            return _viewModelFactory.CreateIndicatorDetailsViewModel(indicator, indicatorTypes, users);
        }

        public Guid Add(IndicatorViewModel indicatorVm)
        {
            var indicator = AutoMapper.Mapper.Map<Indicator>(indicatorVm);
            indicator.Id = Guid.NewGuid();
            _indicatorsRepository.Add(indicator);

            return indicator.Id;
        }

        public void Update(IndicatorViewModel indicatorVm)
        {
            var indicator = _indicatorsRepository.Single(i => i.Id == indicatorVm.Id);
            _indicatorsRepository.Update(AutoMapper.Mapper.Map(indicatorVm, indicator));
        }

        public void Delete(Guid id)
        {
            _indicatorsRepository.Delete(id);
        }

        public IndicatorMeasureDetailsViewModel GetMeasures(Guid indicatorId)
        {
            var indicator = _indicatorsRepository.Single(i => i.Id == indicatorId, i => i.Measures);
            return _viewModelFactory.CreateMeasureDetailsViewModel(indicator);
        }

        public bool AddMeasure(IndicatorMeasureViewModel indicatorMeasureVm)
        {
            var indicatorMeasure = AutoMapper.Mapper.Map<IndicatorMeasure>(indicatorMeasureVm);
            indicatorMeasure.Id = Guid.NewGuid();
            return _indicatorsRepository.AddMeasure(indicatorMeasure);
        }

        public bool UpdateMeasure(IndicatorMeasureViewModel indicatorMeasureVm)
        {
            return _indicatorsRepository.UpdateMeasure(AutoMapper.Mapper.Map<IndicatorMeasure>(indicatorMeasureVm));
        }

        public bool DeleteMeasure(Guid indicatorId, Guid indicatorMeasureId)
        {
            return _indicatorsRepository.DeleteMeasure(indicatorId, indicatorMeasureId);
        }

        public Dictionary<string, string> GetResources()
        {
            return _resourceManager.GetStrings();
        }

        public IQueryable<Indicator> ApplyIndicatorsFilter(IQueryable<Indicator> indicators, string filter)
        {
            return indicators.Where(i => i.Name.Contains(filter) || i.Code.Contains(filter) || i.Description.Contains(filter) ||
                         (i.Manager.Firstname + i.Manager.Surname).Contains(filter));
        }
    }
}
