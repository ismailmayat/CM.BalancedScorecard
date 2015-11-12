namespace CM.BalancedScoreboard.Resources.Abstract
{
    public interface IResourceFactory
    {
        IResourceManager GetResourceManager(ResourceType resourceType);
    }
}
