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
                .ForMember(d => d.LastValue,
                    opt => opt.MapFrom(o => o.Values.OrderByDescending(rv => rv.Date).FirstOrDefault()))
                .ForMember(d => d.Manager,
                    opt => opt.MapFrom(o => string.Format("{0} {1}", o.Manager.Firstname, o.Manager.Surname)));
        }
    }
}
