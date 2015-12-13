namespace CM.BalancedScorecard.Resources.Abstract
{
    public interface IResourceFactory
    {
        IResourceManager GetResourceManager(ResourceType resourceType);
    }
}
