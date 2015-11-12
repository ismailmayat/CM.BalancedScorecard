using CM.BalancedScoreboard.Resources.Abstract;
using System.Collections.Generic;

namespace CM.BalancedScoreboard.Resources.Implementation
{
    public class ResourceFactory : IResourceFactory
    {
        Dictionary<ResourceType, IResourceManager> resources = new Dictionary<ResourceType, IResourceManager>();

        public IResourceManager GetResourceManager(ResourceType resourceType)
        {
            if (!resources.ContainsKey(resourceType))
                resources.Add(resourceType, new LocalResourceManager(GetResourceNamespace(resourceType)));

            return resources[resourceType];
        }

        private string GetResourceNamespace(ResourceType resourceType)
        {
            switch (resourceType)
            {
                case ResourceType.Indicators:
                    return "CM.BalancedScoreboard.Resources.Indicators";
            }
            return string.Empty;
        }
    }
}
