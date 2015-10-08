using System;
using System.Collections.Generic;
using System.Linq;
using CM.BalancedScoreboard.Data.Repository.Abstract;
using CM.BalancedScoreboard.Domain.Model.Indicators;
using CM.BalancedScoreboard.Services.Abstract;
using CM.BalancedScoreboard.Services.Dto;

namespace CM.BalancedScoreboard.Services.Implementation
{
    public class IndicatorService : IIndicatorService
    {
        private readonly IBaseRepository<Indicator> _repository; 
        public IndicatorService(IBaseRepository<Indicator> repository)
        {
            _repository = repository;
        }

        public IEnumerable<IndicatorDto> GetIndicators(string filter)
        {
            var filterList = filter.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var indicators = _repository.Get(i =>
                filterList.Any(
                    f =>
                        i.Name.Contains(f) || i.Code.Contains(f) || i.Description.Contains(f) ||
                        (i.Manager.Firstname + i.Manager.Surname).Contains(f)));

            return indicators.Select(AutoMapper.Mapper.Map<IndicatorDto>);
        }
    }
}
