using System;
using System.Collections.Generic;
using System.Linq;
using CM.BalancedScoreboard.Domain.Model.Enums;
using CM.BalancedScoreboard.Domain.Model.Indicators;
using CM.BalancedScoreboard.Services.ViewModel.Indicators;

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
                    opt => opt.MapFrom(o => o.Manager != null ? o.Manager.Firstname + " " + o.Manager.Surname : string.Empty)).ReverseMap();

            AutoMapper.Mapper.CreateMap<IndicatorMeasure, IndicatorMeasureViewModel>()
                .ForMember(dest => dest.ValueInputType,
                    opt => opt.ResolveUsing(ResolveIndicatorMeasureInputType))
                .ReverseMap();
        }

        public static string ResolveIndicatorMeasureInputType(IndicatorMeasure im)
        {
            switch (im.Indicator.ObjectValueType)
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
