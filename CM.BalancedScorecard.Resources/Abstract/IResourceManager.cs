using System.Collections.Generic;

namespace CM.BalancedScorecard.Resources.Abstract
{
    public interface IResourceManager
    {
        string GetString(string name);

        Dictionary<string, string> GetStrings();
    }
}
