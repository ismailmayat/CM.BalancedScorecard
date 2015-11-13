using CM.BalancedScoreboard.Domain.Model.Enums;
using CM.BalancedScoreboard.Domain.Model.Indicators;
using CM.BalancedScoreboard.Services.ViewModel.Indicators;
using System;
using System.Linq;
using CM.BalancedScoreboard.Services.Mapper.Resolvers;

namespace CM.BalancedScoreboard.Services.Mapper
{
    public static class Mappings
    {
        public static void Configure()
        {
            AutoMapper.Mapper.CreateMap<Indicator, IndicatorViewModel>()
                .ForMember(dest => dest.LastRealValue,
                    opt => opt.MapFrom(o => o.Measures.Any() ? o.Measures.OrderByDescending(rv => rv.Date).FirstOrDefault().RealValue : string.Empty))
                .ForMember(dest => dest.LastTargetValue,
                    opt => opt.MapFrom(o => o.Measures.Any() ? o.Measures.OrderByDescending(rv => rv.Date).FirstOrDefault().TargetValue : string.Empty))
                .ForMember(dest => dest.LastMeasureDate,
                    opt => opt.MapFrom(o => o.Measures.Any() ? o.Measures.OrderByDescending(rv => rv.Date).FirstOrDefault().Date : DateTime.MinValue))
                .ForMember(dest => dest.ManagerName,
                    opt => opt.MapFrom(o => o.Manager != null ? o.Manager.Firstname + " " + o.Manager.Surname : string.Empty))
                .ReverseMap();

            AutoMapper.Mapper.CreateMap<IndicatorMeasure, IndicatorMeasureViewModel>()
               .ForMember(dest => dest.RealValue, opt => opt.ResolveUsing<IndicatorMeasureRealValueResolver>())
               .ForMember(dest => dest.TargetValue, opt => opt.ResolveUsing<IndicatorMeasureTargetValueResolver>())
               .ForMember(dest => dest.ValueInputType, opt => opt.ResolveUsing<ResolveIndicatorMeasureInputTypeResolver>())
               .ReverseMap();
        }
    }
}
