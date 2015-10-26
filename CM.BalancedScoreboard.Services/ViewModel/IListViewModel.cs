using System.Collections.Generic;

namespace CM.BalancedScoreboard.Services.ViewModel
{
    public interface IListViewModel
    {
        IEnumerable<IViewModel> List { get; set; } 
    }
}
