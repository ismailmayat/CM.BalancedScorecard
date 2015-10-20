using CM.BalancedScoreboard.Domain.Model.Enums;
using CM.BalancedScoreboard.Services.ViewModel;
using CM.BalancedScoreboard.Services.ViewModel.Indicators;
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
            var indicatorVm = CreateIndicatorViewModel(ComparisonValueType.Greater, ObjectValueType.Integer, "20", "18");

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
            var indicatorVm = CreateIndicatorViewModel(ComparisonValueType.Smaller, ObjectValueType.Integer, "20", "18");

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
            var indicatorVm = CreateIndicatorViewModel(ComparisonValueType.Greater, ObjectValueType.Decimal, "20.5", "18.2");

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
            var indicatorVm = CreateIndicatorViewModel(ComparisonValueType.Smaller, ObjectValueType.Decimal, "20.5", "18.2");

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
            var indicatorVm = CreateIndicatorViewModel(ComparisonValueType.Equal, ObjectValueType.Boolean, "true", "true");

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
            var indicatorVm = CreateIndicatorViewModel(ComparisonValueType.Equal, ObjectValueType.Boolean, "false", "true");

            //Act
            var result = indicatorVm.State;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Value, IndicatorState.Red);
        }

        private IndicatorViewModel CreateIndicatorViewModel(ComparisonValueType comparisonType,
            ObjectValueType objectType, string lastRecordValue, string lastTargetValue)
        {
            return new IndicatorViewModel()
            {
                ComparisonValueType = comparisonType,
                ObjectValueType = objectType,
                LastRecordValue = lastRecordValue,
                LastTargetValue = lastTargetValue
            };
        }
    }
}
