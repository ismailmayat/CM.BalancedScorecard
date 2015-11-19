using System.ComponentModel.DataAnnotations;

namespace CM.BalancedScoreboard.Services.CustomAttributes
{
    public class CustomDataTypeAttribute : DataTypeAttribute
    {
        public CustomDataTypeAttribute(CDataType dataType) : base(dataType.ToString()) { }
    }
}
