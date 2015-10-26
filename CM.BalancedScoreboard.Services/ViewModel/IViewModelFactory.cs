using System.Linq;
using CM.BalancedScoreboard.Domain.Abstract;

namespace CM.BalancedScoreboard.Services.ViewModel
{
    public interface IViewModelFactory
    {
        IListViewModel CreateListViewModel(IQueryable<IEntity> entities);

        IDetailViewModel CreateDetailViewModel(IEntity entity);
    }
}
