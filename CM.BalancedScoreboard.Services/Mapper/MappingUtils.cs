using CM.BalancedScoreboard.Domain.Abstract;
using CM.BalancedScoreboard.Domain.Model.Indicators;
using CM.BalancedScoreboard.Services.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace CM.BalancedScoreboard.Services.Mapper
{
    public class MappingUtils
    {
        public static void UpdateChilds<TViewModel, TEntity>(List<TViewModel> viewModelChilds, List<TEntity> entityChilds)
            where TViewModel: IViewModel
            where TEntity: IEntity
        {
            foreach (var viewModelChild in viewModelChilds)
            {
                var entityChild = entityChilds.FirstOrDefault(ec => ec.Id == viewModelChild.Id);
                if (entityChild != null)
                {
                    AutoMapper.Mapper.Map(viewModelChild, entityChild);
                }
                else
                {
                    entityChild = AutoMapper.Mapper.Map<TEntity>(viewModelChild);
                }
            }

            entityChilds.RemoveAll(ec => viewModelChilds.All(vmc => vmc.Id != ec.Id));
        }
    }
}
