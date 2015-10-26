using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using CM.BalancedScoreboard.Domain.Abstract;
using CM.BalancedScoreboard.Domain.Model.Indicators;

namespace CM.BalancedScoreboard.Services.ViewModel.Indicators
{
    public class IndicatorViewModelFactory : IViewModelFactory
    {
        public IListViewModel CreateListViewModel(IQueryable<Indicator> entities)
        {
            return new IndicatorListViewModel()
            {
                List = entities.Project().To<IndicatorViewModel>()
            };
        }

        public IDetailViewModel CreateDetailViewModel(IEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
