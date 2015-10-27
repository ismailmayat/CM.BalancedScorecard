using CM.BalancedScoreboard.Domain.Abstract;
using CM.BalancedScoreboard.Services.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CM.BalancedScoreboard.Services.Mapper
{
    public class MappingUtils
    {
        //public static void UpdateChilds<TViewModel, TEntity>(List<TViewModel> viewModelChilds, List<TEntity> entityChilds)
        //    where TViewModel: IViewModel
        //    where TEntity: IChildEntity
        //{
        //    entityChilds.Where(ec => viewModelChilds.All(vmc => vmc.Id != ec.Id)).ToList().ForEach(ec => ec.EntityState = EntityState.Deleted);
        //    foreach (var viewModelChild in viewModelChilds)
        //    {
        //        var entityChild = entityChilds.FirstOrDefault(ec => ec.Id == viewModelChild.Id);
        //        if (entityChild != null)
        //        {
        //            AutoMapper.Mapper.Map(viewModelChild, entityChild);
        //            entityChild.EntityState = EntityState.Modified;
        //        }
        //        else
        //        {
        //            entityChild = AutoMapper.Mapper.Map<TEntity>(viewModelChild);
        //            entityChild.Id = Guid.NewGuid();
        //            entityChild.EntityState = EntityState.Added;
        //            entityChilds.Add(entityChild);
        //        }
        //    }
        //}
    }
}
