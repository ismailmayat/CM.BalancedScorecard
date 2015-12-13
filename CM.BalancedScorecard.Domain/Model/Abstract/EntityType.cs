using System;
using System.ComponentModel.DataAnnotations;

namespace CM.BalancedScorecard.Domain.Abstract
{
    public abstract class EntityType : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
