using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CM.BalancedScorecard.Domain.Model.Indicators;
using CM.BalancedScorecard.Domain.Model.Users;

namespace CM.BalancedScorecard.Services.Mapper.Resolvers
{
    public class IndicatorMeasureLastRealValueResolver : ValueResolver<List<IndicatorMeasure>, string>
    {
        protected override string ResolveCore(List<IndicatorMeasure> source)
        {
            return source.Any() ? source.OrderByDescending(rv => rv.Date).First().RealValue : string.Empty;
        }
    }
    public class IndicatorMeasureLastTargetValueResolver : ValueResolver<List<IndicatorMeasure>, string>
    {
        protected override string ResolveCore(List<IndicatorMeasure> source)
        {
            return source.Any() ? source.OrderByDescending(rv => rv.Date).First().TargetValue : string.Empty;
        }
    }

    public class IndicatorMeasureLastDateValueResolver : ValueResolver<List<IndicatorMeasure>, DateTime?>
    {
        protected override DateTime? ResolveCore(List<IndicatorMeasure> source)
        {
            return source.Any() ? source.OrderByDescending(rv => rv.Date).First().Date : (DateTime?)null;
        }
    }

    public class IndicatorManagerNameResolver : ValueResolver<User, string>
    {
        protected override string ResolveCore(User source)
        {
            return source != null ? source.Firstname + " " + source.Surname : string.Empty;
        }
    }
}
