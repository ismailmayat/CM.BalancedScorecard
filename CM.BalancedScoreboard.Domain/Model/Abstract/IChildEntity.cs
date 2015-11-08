using System;

namespace CM.BalancedScoreboard.Domain.Abstract
{
    public enum EntityState
    {
        Unchanged = 0,
        Added = 1,
        Modified = 2,
        Deleted = 3
    }

    public interface IChildEntity
    {
        Guid Id { get; set; }
        EntityState EntityState { get; set; }
    }
}
