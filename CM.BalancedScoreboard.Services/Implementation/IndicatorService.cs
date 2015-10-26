using System;
using System.Linq.Expressions;
using AutoMapper.QueryableExtensions;
using CM.BalancedScoreboard.Data.Repository.Abstract;
using CM.BalancedScoreboard.Domain.Model.Indicators;
using CM.BalancedScoreboard.Services.Abstract;
using CM.BalancedScoreboard.Services.ViewModel.Indicators;

namespace CM.BalancedScoreboard.Services.Implementation
{
    public class IndicatorService : CrudService<IndicatorListViewModel, IndicatorDetailsViewModel, Indicator>, IIndicatorService
    {
        private readonly IIndicatorRepository _repository;

        public IndicatorService(IIndicatorRepository repository)
        {
            _repository = repository;
        }

        public override IndicatorListViewModel GetList(string filter)
        {
            var indicators =_repository.Get(CreateFilter(filter));

            return new IndicatorListViewModel()
            {
                List = indicators.Project().To<IndicatorViewModel>()
            };
        }

        public override IndicatorDetailsViewModel GetSingle(Guid id)
        {
            var indicator = _repository.Single(i => i.Id == id, i => i.Values);

            return new IndicatorDetailsViewModel()
            {
                Model = AutoMapper.Mapper.Map<IndicatorViewModel>(indicator)
            };
        }

        public override Expression<Func<Indicator, bool>> CreateFilter(string filter)
        {
            return i => i.Name.Contains(filter) || i.Code.Contains(filter) || i.Description.Contains(filter) ||
                        (i.Manager.Firstname + i.Manager.Surname).Contains(filter);
        }
    }
}
