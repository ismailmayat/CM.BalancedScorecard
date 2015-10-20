using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CM.BalancedScoreboard.Data.Repository.Abstract;
using CM.BalancedScoreboard.Domain.Model.Indicators;
using CM.BalancedScoreboard.Services.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CM.BalancedScoreboard.Services.Tests.Indicators
{
    [TestClass]
    public class IndicatorServiceTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Mapper.Mappings.Configure();    
        }

        [TestMethod]
        public void Can_Return_And_Map_Indicators()
        {
            //Arrange
            var repo = new Mock<IIndicatorRepository>();
            repo.Setup(r => r.Get(It.IsAny<Expression<Func<Indicator, bool>>>())).Returns(GetIndicatorList());
            var service = new IndicatorService(repo.Object);
            var filter = "Indicator 1";//string.Empty;

            //Act
            var result = service.GetIndicators(filter);

            //Assert
            Assert.AreEqual(result.Count(), 4);
            Assert.IsTrue(result[0].Name == "Indicator 1");
            Assert.IsTrue(result[0].Values.Count() == 1);
        }

        [TestMethod]
        public void Can_Return_And_Map_Indicator()
        {
            //Arrange
            var id = new Guid();
            var repo = new Mock<IIndicatorRepository>();
            repo.Setup(
                r =>
                    r.Single(It.IsAny<Expression<Func<Indicator, bool>>>(),
                        It.IsAny<Expression<Func<Indicator, object>>>())).Returns(GetIndicator(id));
            var service = new IndicatorService(repo.Object);            

            //Act
            var result = service.GetIndicator(id);

            //Assert
            Assert.IsTrue(result.Indicator.Name == "Indicator 1");
            Assert.IsTrue(result.Indicator.Values.Count() == 1);
        }

        private static Indicator GetIndicator(Guid id)
        {
            return new Indicator()
            {
                Id = id,
                Name = "Indicator 1",
                Code = "000",
                Values = new List<IndicatorValue>()
                {
                    new IndicatorValue()
                    {
                        Id = Guid.NewGuid(),
                        Date = DateTime.Today,
                        RecordValue = "20",
                        TargetValue = "14"
                    }
                }
            };
        }

        private static IQueryable<Indicator> GetIndicatorList()
        {
            return new List<Indicator>()
            {
                new Indicator()
                {
                    Id = Guid.NewGuid(),
                    Name = "Indicator 1",
                    Code = "000",
                    Values = new List<IndicatorValue>()
                    {
                        new IndicatorValue()
                        {
                            Id = Guid.NewGuid(),
                            Date = DateTime.Today,
                            RecordValue = "20",
                            TargetValue = "14"
                        }
                    }
                },
                new Indicator()
                {
                    Id = Guid.NewGuid(),
                    Name = "Indicator 2",
                    Code = "000",
                    Values = new List<IndicatorValue>()
                    {
                        new IndicatorValue()
                        {
                            Id = Guid.NewGuid(),
                            Date = DateTime.Today,
                            RecordValue = "15",
                            TargetValue = "16"
                        }
                    }
                },
                new Indicator()
                {
                    Id = Guid.NewGuid(),
                    Name = "Indicator 3",
                    Code = "001",
                    Values = new List<IndicatorValue>()
                    {
                        new IndicatorValue()
                        {
                            Id = Guid.NewGuid(),
                            Date = DateTime.Today,
                            RecordValue = "9",
                            TargetValue = "114"
                        }
                    }
                },
                new Indicator()
                {
                    Id = Guid.NewGuid(),
                    Name = "Indicator 4",
                    Code = "001",
                    Values = new List<IndicatorValue>()
                    {
                        new IndicatorValue()
                        {
                            Id = Guid.NewGuid(),
                            Date = DateTime.Today,
                            RecordValue = "21",
                            TargetValue = "84"
                        }
                    }
                }
            }.AsQueryable();
        }
    }
}
