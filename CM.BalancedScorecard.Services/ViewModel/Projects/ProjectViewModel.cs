using CM.BalancedScorecard.Domain.Model.Projects;
using System;
using System.Collections.Generic;

namespace CM.BalancedScorecard.Services.ViewModel.Projects
{
    public class ProjectViewModel : IViewModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool Active { get; set; }
        public DateTime PlannedStartDate { get; set; }
        public DateTime PlannedEndDate { get; set; }
        public DateTime? RealStartDate { get; set; }
        public DateTime? RealEndDate { get; set; }
        public decimal PlannedBudget { get; set; }
        public decimal? RealBudget { get; set; }
        public Guid ManagerId { get; set; }
        public List<Milestone> Milestones { get; set; }
    }
}
