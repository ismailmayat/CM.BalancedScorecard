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

    public enum ValueComparisonType
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

    public enum ValueObjectType
    {
        Integer,
        Decimal,
        Boolean
    }
}
