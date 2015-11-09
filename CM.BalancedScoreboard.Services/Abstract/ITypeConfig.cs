using System.Collections.Generic;

namespace CM.BalancedScoreboard.Services.Abstract
{
    public interface ITypeConfig
    {
        Dictionary<string, Dictionary<string, object>> GetAttributes<T>() where T : class;
    }
}
