using System.Reflection;
using System.Resources;

namespace CM.BalancedScoreboard.Resources
{
    public class LocalResourceManager : IResourceManager
    {
        private readonly ResourceManager _resourceManager;

        public LocalResourceManager()
        {
            _resourceManager =  new ResourceManager("CM.BalancedScoreboard.Resources.Resources", Assembly.GetExecutingAssembly());
        }

        public string GetString(string name)
        {
            return _resourceManager.GetString(name);
        }
    }
}
