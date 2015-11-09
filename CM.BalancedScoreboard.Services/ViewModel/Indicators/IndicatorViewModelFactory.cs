using CM.BalancedScoreboard.Domain.Abstract.Indicators;
using CM.BalancedScoreboard.Domain.Model.Enums;
using CM.BalancedScoreboard.Domain.Model.Indicators;
using CM.BalancedScoreboard.Resources;
using CM.BalancedScoreboard.Services.Abstract;
using CM.BalancedScoreboard.Services.Abstract.Indicators;
using CM.BalancedScoreboard.Services.Utils;

namespace CM.BalancedScoreboard.Services.ViewModel.Indicators
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

        public IndicatorDetailsViewModel CreateDetailsViewModel(Indicator indicator)
        {
            var indicatorVm = AutoMapper.Mapper.Map<IndicatorViewModel>(indicator);

            indicatorVm.State = _stateCalculator.Calculate(indicatorVm.LastMeasureDate, indicatorVm.LastRealValue,
                indicatorVm.LastTargetValue, indicatorVm.PeriodicityType, indicatorVm.ComparisonValueType,
                indicatorVm.ObjectValueType, indicatorVm.FulfillmentRate);

            return new IndicatorDetailsViewModel()
            {
                Indicator = indicatorVm,
                Config = _typeConfig.GetAttributes<IndicatorViewModel>(),
                PeriodicityTypeList = EnumUtil<PeriodicityType>.GetOptions(_resourceManager),
                ComparisonValueTypeList = EnumUtil<ComparisonValueType>.GetOptions(_resourceManager),
                ObjectValueTypeList = EnumUtil<ObjectValueType>.GetOptions(_resourceManager)
            };
        }
    }
}
