using System.Collections.Generic;
using CM.BalancedScoreboard.Domain.Model.Enums;
using CM.BalancedScoreboard.Services.Utils;

namespace CM.BalancedScoreboard.Services.ViewModel.Indicators
{
    public class IndicatorDetailsViewModel : IDetailViewModel
    {
        public IViewModel Model { get; set; }

        public IEnumerable<object> PeriodicityTypeList => EnumUtil.GetOptions<PeriodicityType>();

        public IEnumerable<object> ComparisonValueTypeList => EnumUtil.GetOptions<ComparisonValueType>();

        public IEnumerable<object> ObjectValueTypeList => EnumUtil.GetOptions<ObjectValueType>();

        public IEnumerable<object> SplitTypeList => EnumUtil.GetOptions<SplitType>();
    }
}
