using AutoMapper.QueryableExtensions;
using CM.BalancedScorecard.Domain.Abstract.Indicators;
using CM.BalancedScorecard.Domain.Model.Enums;
using CM.BalancedScorecard.Domain.Model.Indicators;
using CM.BalancedScorecard.Resources;
using CM.BalancedScorecard.Resources.Abstract;
using CM.BalancedScorecard.Services.Abstract;
using CM.BalancedScorecard.Services.Abstract.Indicators;
using CM.BalancedScorecard.Services.Utils;
using CM.BalancedScorecard.Services.ViewModel.Indicators;
using System.Collections.Generic;
using System.Linq;
using CM.BalancedScorecard.Domain.Model.Users;
using System;

namespace CM.BalancedScorecard.Services.Implementation.Indicators
{
    public class IndicatorViewModelFactory : IIndicatorViewModelFactory
    {
        readonly ITypeConfig _typeConfig;
        readonly IResourceManager _resourceManager;
        readonly IIndicatorStateCalculator _stateCalculator;

        public IndicatorViewModelFactory(ITypeConfig typeConfig, IResourceFactory resourceFactory, IIndicatorStateCalculator stateCalculator)
        {
            _typeConfig = typeConfig;
            _resourceManager = resourceFactory.GetResourceManager(ResourceType.Indicators);
            _stateCalculator = stateCalculator;
        }

        public IndicatorDetailsViewModel CreateIndicatorDetailsViewModel(Indicator indicator, IQueryable<IndicatorType> indicatorTypes, IQueryable<User> users)
        {
            var indicatorVm = AutoMapper.Mapper.Map<IndicatorViewModel>(indicator);

            indicatorVm.State = _stateCalculator.Calculate(indicatorVm.LastMeasureDate, indicatorVm.LastRealValue,
                indicatorVm.LastTargetValue, indicatorVm.PeriodicityType, indicatorVm.ComparisonValueType,
                indicatorVm.ObjectValueType, indicatorVm.FulfillmentRate);

            return new IndicatorDetailsViewModel()
            {
                Data = indicatorVm,
                Config = _typeConfig.GetAttributes<IndicatorViewModel>(),
                IndicatorTypes = indicatorTypes.OrderBy(it => it.Name).Select(it => new Option() { Id = it.Id.ToString(), Name = it.Name }).ToList(),
                Users = users.OrderBy(u => u.Surname).Select(u => new Option() { Id = u.Id.ToString(), Name = u.Firstname + " " + u.Surname }).ToList()
            };
        }

        public IndicatorMeasureDetailsViewModel CreateMeasureDetailsViewModel(Indicator indicator)
        {
            var data = indicator?.Measures.GroupBy(i => i.Date.Year)
                .Select(gb => new IndicatorMeasureListViewModel()
                {
                    Year = gb.Key,
                    Measures = AutoMapper.Mapper.Map<List<IndicatorMeasureViewModel>>(gb).OrderBy(im => im.Date).ToList()
                }).OrderByDescending(im => im.Year).ToList();

            return new IndicatorMeasureDetailsViewModel()
            {
                Data = data,
                Config = _typeConfig.GetAttributes<IndicatorMeasureViewModel>()
            };
        }

        public IndicatorListViewModel CreateIndicatorListViewModel(IQueryable<Indicator> indicators)
        {
            var indicatorVms = indicators.Project().To<IndicatorViewModel>().ToList();
            foreach (var indicatorVm in indicatorVms)
            {
                indicatorVm.State = _stateCalculator.Calculate(indicatorVm.LastMeasureDate, indicatorVm.LastRealValue,
                indicatorVm.LastTargetValue, indicatorVm.PeriodicityType, indicatorVm.ComparisonValueType,
                indicatorVm.ObjectValueType, indicatorVm.FulfillmentRate);
            }

            return new IndicatorListViewModel()
            {
                Data = indicatorVms
            };
        }
    }
}
