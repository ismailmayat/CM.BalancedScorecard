using System.ComponentModel.DataAnnotations;

namespace CM.BalancedScoreboard.Domain.Model.Enums
{
    public enum PeriodicityType
    {
        [Display (Name = "Weekly")]
        Week,
        [Display(Name = "Every 2 weeks")]
        TwoWeek,
        [Display(Name = "Monthly")]
        Month,
        [Display(Name = "2 months")]
        TwoMonth,
        [Display(Name = "3 months")]
        ThreeMonth,
        [Display(Name = "4 months")]
        FourMonth,
        [Display(Name = "Half year")]
        SixMonth,
        [Display(Name = "Yearly")]
        TwelveMonth
    }

    public enum ComparisonValueType
    {
        Greater,
        Smaller,
        Equal,
        Inbound ,
        Outbound
    }

    public enum SplitType
    {
        Sum,
        Avg
    }

    public enum ObjectValueType
    {
        Integer,
        Decimal,
        Boolean
    }

    public enum State
    {
        Grey,
        Green,
        Yellow,
        Red
    }
}
