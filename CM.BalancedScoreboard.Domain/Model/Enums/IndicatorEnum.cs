using System.ComponentModel.DataAnnotations;

namespace CM.BalancedScoreboard.Domain.Model.Enums
{
    public enum PeriodicityType
    {
        [Display(Name = "Monthly")]
        Month,
        [Display(Name = "TwoMonth")]
        TwoMonth,
        [Display(Name = "ThreeMonth")]
        ThreeMonth,
        [Display(Name = "FourMonth")]
        FourMonth,
        [Display(Name = "SixMonth")]
        SixMonth,
        [Display(Name = "TwelveMonth")]
        TwelveMonth
    }

    public enum ComparisonValueType
    {
        [Display(Name = "Greater")]
        Greater,
        [Display(Name = "Smaller")]
        Smaller,
        [Display(Name = "Equal")]
        Equal,
    }

    public enum SplitType
    {
        Sum,
        Avg
    }

    public enum ObjectValueType
    {
        [Display(Name = "Integer")]
        Integer,
        [Display(Name = "Decimal")]
        Decimal,
        [Display(Name = "Boolean")]
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
