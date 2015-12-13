using System.Collections.Generic;

namespace CM.BalancedScorecard.Services.Abstract
{
    public interface ITypeConfig
    {
        Dictionary<string, Dictionary<string, object>> GetAttributes<T>() where T : class;
    }
}
