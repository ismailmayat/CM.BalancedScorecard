using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
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
            //var filterList = filter.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            //var indicators = _repository.Get(i =>
            //    filterList.Any(
            //        f =>
            //            i.Name.Contains(f) || i.Code.Contains(f) || i.Description.Contains(f) ||
            //            (i.Manager.Firstname + i.Manager.Surname).Contains(f)));

            var indicators =
                _repository.Get(
                    i => i.Name.Contains(filter) || i.Code.Contains(filter) || i.Description.Contains(filter) ||
                         (i.Manager.Firstname + i.Manager.Surname).Contains(filter));

            return indicators.Project().To<IndicatorDto>().ToList();
        }
    }
}
