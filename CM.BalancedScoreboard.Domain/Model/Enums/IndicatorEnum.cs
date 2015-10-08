namespace CM.BalancedScoreboard.Domain.Model.Enums
{
    public enum Periodicity
    {
        Week,
        TwoWeek,
        Month,
        TwoMonth,
        ThreeMonth,
        SixMonth,
        TwelveMonth
    }

    public enum TargetType
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

    public enum ValueType
    {
        Integer,
        Decimal,
        Boolean
    }
}
