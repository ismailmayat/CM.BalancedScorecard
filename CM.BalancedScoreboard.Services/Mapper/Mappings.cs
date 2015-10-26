﻿using System.Linq;
using CM.BalancedScoreboard.Domain.Model.Indicators;
using CM.BalancedScoreboard.Services.ViewModel.Indicators;

namespace CM.BalancedScoreboard.Services.Mapper
{
    public static class Mappings
    {
        public static void Configure()
        {
            AutoMapper.Mapper.CreateMap<Indicator, IndicatorViewModel>()
                .ForMember(dest => dest.LastRecordValue,
                    opt => opt.MapFrom(o => o.Values.Any() ? o.Values.OrderByDescending(rv => rv.Date).FirstOrDefault().RecordValue : string.Empty))
                .ForMember(dest => dest.LastTargetValue,
                    opt => opt.MapFrom(o => o.Values.Any() ? o.Values.OrderByDescending(rv => rv.Date).FirstOrDefault().TargetValue : string.Empty))
                .ForMember(dest => dest.ManagerName,
                    opt => opt.MapFrom(o => o.Manager != null ? o.Manager.Firstname + " " + o.Manager.Surname : string.Empty));

            AutoMapper.Mapper.CreateMap<IndicatorValue, IndicatorValueViewModel>();

            AutoMapper.Mapper.CreateMap<IndicatorViewModel, Indicator>()
                .ForMember(dest => dest.Values,
                    opt => opt.Ignore());
        }
    }
}
