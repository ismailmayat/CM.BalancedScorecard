using System;

namespace CM.BalancedScoreboard.Domain.Abstract
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
