using System;
using CM.BalancedScoreboard.Domain.Abstract;
using CM.BalancedScoreboard.Services.ViewModel;

namespace CM.BalancedScoreboard.Services.Abstract
{
    public interface ICrudService<TListViewModel, TDetailViewModel, TEntity> 
        where TListViewModel : class, IListViewModel
        where TDetailViewModel : class, IDetailViewModel
        where TEntity : class, IEntity
    {
        TListViewModel GetList(string filter);

        TDetailViewModel GetSingle(Guid id);

        void Add(TDetailViewModel viewModel);

        void Update(Guid id, TDetailViewModel viewModel);

        void Delete(Guid id);
    }
}
