namespace CM.BalancedScoreboard.Domain.Model.Enums
{
    public enum PeriodicityType
    {
        Week,
        TwoWeek,
        Month,
        TwoMonth,
        ThreeMonth,
        SixMonth,
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

    public enum ObjectValueType
    {
        Integer,
        Decimal,
        Boolean
    }
}
