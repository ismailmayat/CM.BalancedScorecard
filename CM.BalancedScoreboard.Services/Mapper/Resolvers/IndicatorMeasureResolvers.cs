using AutoMapper;
using CM.BalancedScoreboard.Domain.Model.Enums;
using CM.BalancedScoreboard.Domain.Model.Indicators;

namespace CM.BalancedScoreboard.Services.Mapper.Resolvers
{
    public class IndicatorMeasureRealValueResolver : ValueResolver<IndicatorMeasure, object>
    {
        protected override object ResolveCore(IndicatorMeasure source)
        {
            switch (source.Indicator.ObjectValueType)
            {
                case ObjectValueType.Decimal:
                    return decimal.Parse(source.RealValue);
                case ObjectValueType.Integer:
                    return int.Parse(source.RealValue);
                default:
                    return source;
            }
        }
    }

    public class IndicatorMeasureTargetValueResolver : ValueResolver<IndicatorMeasure, object>
    {
        protected override object ResolveCore(IndicatorMeasure source)
        {
            switch (source.Indicator.ObjectValueType)
            {
                case ObjectValueType.Decimal:
                    return decimal.Parse(source.TargetValue);
                case ObjectValueType.Integer:
                    return int.Parse(source.TargetValue);
                default:
                    return source;
            }
        }
    }

    public class ResolveIndicatorMeasureInputTypeResolver : ValueResolver<IndicatorMeasure, string>
    {
        protected override string ResolveCore(IndicatorMeasure source)
        {
            switch (source.Indicator.ObjectValueType)
            {
                case ObjectValueType.Decimal:
                case ObjectValueType.Integer:
                    return "number";
                default:
                    return "text";
            }
        }
    }
}
