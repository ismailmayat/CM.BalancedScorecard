using System.ComponentModel.DataAnnotations;

namespace CM.BalancedScoreboard.Domain.Model.Enums
{
    public enum PeriodicityType
    {
        [Display(Name = "Monthly")]
        Month = 1,
        [Display(Name = "TwoMonth")]
        TwoMonth = 2,
        [Display(Name = "ThreeMonth")]
        ThreeMonth = 3,
        [Display(Name = "FourMonth")]
        FourMonth = 4,
        [Display(Name = "SixMonth")]
        SixMonth = 6,
        [Display(Name = "TwelveMonth")]
        TwelveMonth = 12
    }

    public enum ComparisonValueType
    {
        [Display(Name = "Greater")]
        Greater = 1,
        [Display(Name = "Smaller")]
        Smaller = 2,
        [Display(Name = "Equal")]
        Equal = 3,
    }

    public enum SplitType
    {
        Sum,
        Avg
    }

    public enum ObjectValueType
    {
        [Display(Name = "Integer")]
        Integer = 1,
        [Display(Name = "Decimal")]
        Decimal = 2,
        [Display(Name = "Boolean")]
        Boolean = 3
    }

    public enum State
    {
        Grey,
        Green,
        Yellow,
        Red
    }
}
