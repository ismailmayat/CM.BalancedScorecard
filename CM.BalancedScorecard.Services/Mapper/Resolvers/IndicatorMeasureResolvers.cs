﻿using AutoMapper;
using CM.BalancedScorecard.Domain.Model.Enums;
using CM.BalancedScorecard.Domain.Model.Indicators;

namespace CM.BalancedScorecard.Services.Mapper.Resolvers
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
                    return source.TargetValue;
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
                    return source.TargetValue;
            }
        }
    }
}
