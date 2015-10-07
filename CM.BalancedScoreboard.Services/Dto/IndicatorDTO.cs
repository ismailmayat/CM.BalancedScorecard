using System;
using CM.BalancedScoreboard.Domain.Model.Indicators;

namespace CM.BalancedScoreboard.Services.Dto
{
    public class IndicatorDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Manager { get; set; }
        public IndicatorState State { get; set; }
        public IndicatorValue LastValue { get; set; }
    }
}
