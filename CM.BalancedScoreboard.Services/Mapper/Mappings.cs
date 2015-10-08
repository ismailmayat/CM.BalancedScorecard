using System.Linq;
using CM.BalancedScoreboard.Domain.Model.Indicators;
using CM.BalancedScoreboard.Services.Dto;

namespace CM.BalancedScoreboard.Services.Mapper
{
    public static class Mappings
    {
        public static void Configure()
        {
            AutoMapper.Mapper.CreateMap<Indicator, IndicatorDto>()
                .ForMember(d => d.LastRecordValue,
                    opt => opt.MapFrom(o => o.Values.Any() ? o.Values.OrderByDescending(rv => rv.Date).FirstOrDefault().RecordValue : string.Empty))
                .ForMember(d => d.LastTargetValue,
                    opt => opt.MapFrom(o => o.Values.Any() ? o.Values.OrderByDescending(rv => rv.Date).FirstOrDefault().TargetValue : string.Empty))
                .ForMember(d => d.ManagerName,
                    opt => opt.MapFrom(o => o.Manager.Firstname + " " + o.Manager.Surname));
        }
    }
}
