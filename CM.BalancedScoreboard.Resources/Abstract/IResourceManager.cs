using System.Collections.Generic;

namespace CM.BalancedScoreboard.Resources.Abstract
{
    public interface IResourceManager
    {
        string GetString(string name);

        Dictionary<string, string> GetStrings();
    }
}
