using System;
using System.Linq.Expressions;
using CM.BalancedScoreboard.Data.Repository.Abstract;
using CM.BalancedScoreboard.Domain.Abstract;
using CM.BalancedScoreboard.Services.Abstract;
using CM.BalancedScoreboard.Services.ViewModel;

namespace CM.BalancedScoreboard.Services.Implementation
{
    public abstract class CrudService<TListViewModel, TDetailViewModel, TEntity> : ICrudService<TListViewModel, TDetailViewModel, TEntity>
        where TListViewModel : class, IListViewModel
        where TDetailViewModel : class, IDetailViewModel
        where TEntity : class, IEntity
    {
        private readonly IBaseRepository<TEntity> _repository;

        protected CrudService() { }

        protected CrudService(IBaseRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public virtual void Add(TDetailViewModel viewModel)
        {
            _repository.Add(AutoMapper.Mapper.Map<TEntity>(viewModel.Model));
        }

        public virtual void Delete(Guid id)
        {
            _repository.Delete(id);
        }

        public virtual TDetailViewModel GetSingle(Guid id)
        {
            var entity = _repository.Single(i => i.Id == id);


        }

        public virtual TListViewModel GetList(string filter)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(Guid id, TDetailViewModel viewModel)
        {
            var entity = _repository.Single(i => i.Id == id);
            _repository.Update(AutoMapper.Mapper.Map(viewModel, entity));
        }

        public abstract Expression<Func<TEntity, bool>> CreateFilter(string filter);
    }
}
