using CM.BalancedScoreboard.Domain.Model.Enums;
using CM.BalancedScoreboard.Services.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CM.BalancedScoreboard.Services.Tests.Indicators
{
    [TestClass]
    public class IndicatorViewModelTest
    {
        [TestMethod]
        public void Can_Calculate_State_With_Int_Green()
        {
            //Arrange
            var indicatorVm = CreateIndicatorViewModel(ValueComparisonType.Greater, ValueObjectType.Integer, "20", "18");

            //Act
            var result = indicatorVm.State;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Value, IndicatorState.Green);
        }

        [TestMethod]
        public void Can_Calculate_State_With_Int_Red()
        {
            //Arrange
            var indicatorVm = CreateIndicatorViewModel(ValueComparisonType.Smaller, ValueObjectType.Integer, "20", "18");

            //Act
            var result = indicatorVm.State;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Value, IndicatorState.Red);
        }

        [TestMethod]
        public void Can_Calculate_State_With_Double_Green()
        {
            //Arrange
            var indicatorVm = CreateIndicatorViewModel(ValueComparisonType.Greater, ValueObjectType.Decimal, "20.5", "18.2");

            //Act
            var result = indicatorVm.State;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Value, IndicatorState.Green);
        }

        [TestMethod]
        public void Can_Calculate_State_With_Double_Red()
        {
            //Arrange
            var indicatorVm = CreateIndicatorViewModel(ValueComparisonType.Smaller, ValueObjectType.Decimal, "20.5", "18.2");

            //Act
            var result = indicatorVm.State;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Value, IndicatorState.Red);
        }

        [TestMethod]
        public void Can_Calculate_State_With_Boolean_Green()
        {
            //Arrange
            var indicatorVm = CreateIndicatorViewModel(ValueComparisonType.Equal, ValueObjectType.Boolean, "true", "true");

            //Act
            var result = indicatorVm.State;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Value, IndicatorState.Green);
        }

        [TestMethod]
        public void Can_Calculate_State_With_Boolean_Red()
        {
            //Arrange
            var indicatorVm = CreateIndicatorViewModel(ValueComparisonType.Equal, ValueObjectType.Boolean, "false", "true");

            //Act
            var result = indicatorVm.State;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Value, IndicatorState.Red);
        }

        private IndicatorViewModel CreateIndicatorViewModel(ValueComparisonType comparisonType,
            ValueObjectType objectType, string lastRecordValue, string lastTargetValue)
        {
            return new IndicatorViewModel()
            {
                ValueComparisonType = comparisonType,
                ValueObjectType = objectType,
                LastRecordValue = lastRecordValue,
                LastTargetValue = lastTargetValue
            };
        }
    }
}
