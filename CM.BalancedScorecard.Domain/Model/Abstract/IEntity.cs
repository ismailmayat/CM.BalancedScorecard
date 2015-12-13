using System;

namespace CM.BalancedScorecard.Domain.Abstract
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
