﻿using CM.BalancedScoreboard.Domain.Abstract.Indicators;
using CM.BalancedScoreboard.Domain.Model.Enums;
using CM.BalancedScoreboard.Domain.Model.Indicators;
using CM.BalancedScoreboard.Resources;
using CM.BalancedScoreboard.Services.Abstract;
using CM.BalancedScoreboard.Services.Abstract.Indicators;
using CM.BalancedScoreboard.Services.Utils;
using CM.BalancedScoreboard.Services.ViewModel.Indicators;
using System.Collections.Generic;
using System.Linq;

namespace CM.BalancedScoreboard.Services.Implementation.Indicators
{
    public class IndicatorViewModelFactory : IIndicatorViewModelFactory
    {
        readonly ITypeConfig _typeConfig;
        readonly IResourceManager _resourceManager;
        private readonly IIndicatorStateCalculator _stateCalculator;

        public IndicatorViewModelFactory(ITypeConfig typeConfig, IResourceManager resourceManager, IIndicatorStateCalculator stateCalculator)
        {
            _typeConfig = typeConfig;
            _resourceManager = resourceManager;
            _stateCalculator = stateCalculator;
        }

        public IndicatorDetailsViewModel CreateIndicatorDetailsViewModel(Indicator indicator)
        {
            var indicatorVm = AutoMapper.Mapper.Map<IndicatorViewModel>(indicator);

            indicatorVm.State = _stateCalculator.Calculate(indicatorVm.LastMeasureDate, indicatorVm.LastRealValue,
                indicatorVm.LastTargetValue, indicatorVm.PeriodicityType, indicatorVm.ComparisonValueType,
                indicatorVm.ObjectValueType, indicatorVm.FulfillmentRate);

            return new IndicatorDetailsViewModel()
            {
                Data = indicatorVm,
                Config = _typeConfig.GetAttributes<IndicatorViewModel>(),
                PeriodicityTypeList = EnumUtil<PeriodicityType>.GetOptions(_resourceManager),
                ComparisonValueTypeList = EnumUtil<ComparisonValueType>.GetOptions(_resourceManager),
                ObjectValueTypeList = EnumUtil<ObjectValueType>.GetOptions(_resourceManager),
                Resources = CreateDetailsViewResources()
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
                Config = _typeConfig.GetAttributes<IndicatorMeasureViewModel>(),
                Resources = CreateMeasuresViewResources()
            };
        }

        private Dictionary<string, string> CreateDetailsViewResources()
        {
            return new Dictionary<string, string>()
            {
                {
                    "Save", _resourceManager.GetString("Save")
                },
                {
                    "Delete", _resourceManager.GetString("Delete")
                },
                {
                    "Details", _resourceManager.GetString("Details")
                }
            };
        }

        private Dictionary<string, string> CreateMeasuresViewResources()
        {
            return new Dictionary<string, string>()
            {
                {
                    "RealValue", _resourceManager.GetString("RealValue")
                },
                {
                    "TargetValue", _resourceManager.GetString("TargetValue")
                },
                {
                    "Measures", _resourceManager.GetString("Measures")
                },
                {
                    "Date", _resourceManager.GetString("Date")
                },
                {
                    "Actions", _resourceManager.GetString("Actions")
                },
                {
                    "AddPeriod", _resourceManager.GetString("AddPeriod")
                },
                {
                    "Show", _resourceManager.GetString("Show")
                },
            };
        }
    }
}