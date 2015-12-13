using System.ComponentModel.DataAnnotations;

namespace CM.BalancedScorecard.Services.CustomAttributes
{
    public class CustomDataTypeAttribute : DataTypeAttribute
    {
        public CustomDataTypeAttribute(CDataType dataType) : base(dataType.ToString()) { }
    }
}
