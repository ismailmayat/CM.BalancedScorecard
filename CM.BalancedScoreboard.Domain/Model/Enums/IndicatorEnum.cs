
namespace CM.BalancedScoreboard.Domain.Model.Enums
{
    public enum Periodicity
    {
        Week = 1,
        TwoWeek = 2,
        Month = 3,
        TwoMonth = 4,
        ThreeMonth = 5,
        SixMonth = 6,
        TwelveMonth = 7
    }

    public enum TargetType
    {
        Greater = 1,
        Smaller = 2,
        Equal = 3,
        Inbound = 4,
        Outbound = 5
    }

    public enum State
    {
        Green,
        Yellow,
        Red
    }

    public enum SplitType
    {
        Sum,
        Avg
    }
}
