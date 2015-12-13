using CM.BalancedScorecard.Resources.Abstract;
using System.Collections.Generic;

namespace CM.BalancedScorecard.Resources.Implementation
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
                    return "CM.BalancedScorecard.Resources.Indicators";
            }
            return string.Empty;
        }
    }
}
