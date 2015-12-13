using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CM.BalancedScoreboard.Data.Repository.Abstract;
using CM.BalancedScoreboard.Data.Repository.Implementation.Indicators;
using CM.BalancedScoreboard.Domain.Model.Indicators;
using Moq;
using NUnit.Framework;

namespace CM.BalancedScoreboard.Data.Tests
{
    [TestFixture]
    public class RepositoryTest
    {
        IEnumerable<Indicator> indicatorList;
        Mock<IDbSet<Indicator>> dbSet;
        Mock<IDbContext> dbContext;
        Mock<IUnitOfWork> uof;

        [SetUp]
        public void SetUp()
        {
            indicatorList = Shared.GetIndicatorList();
            dbSet = Shared.GetDbSetTest<Indicator>(indicatorList);
            dbContext = Shared.GetDbContextTest<Indicator>(dbSet.Object);
            uof = Shared.GetUnitfOfWorkTest(dbContext.Object);
        }

        [Test]
        public void Can_Return_All()
        {
            //Arrange
            var iRepo = new IndicatorsRepository(uof.Object);

            //Act
            var result = iRepo.Get();

            //Assert
            Assert.AreEqual(result.Count(), 4);
        }

        [Test]
        public void Can_Return_Filtered()
        {
            //Arrange
            var iRepo = new IndicatorsRepository(uof.Object);

            //Act
            var result = iRepo.Get(i => i.Code.Equals("001", StringComparison.InvariantCultureIgnoreCase));

            //Assert
            Assert.AreEqual(result.Count(), 1);
            Assert.IsTrue(result.ToList().First().Code.Equals("001", StringComparison.InvariantCultureIgnoreCase));
        }

        [Test]
        public void Can_Return_Single()
        {
            //Arrange
            var iRepo = new IndicatorsRepository(uof.Object);

            //Act
            var result = iRepo.Single(i => i.Name.Equals("indicator 1", StringComparison.InvariantCultureIgnoreCase));

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Name.Equals("indicator 1", StringComparison.InvariantCultureIgnoreCase));
        }

        [Test]
        public void Can_Return_Single_With_Navigation_Property()
        {
            //Arrange
            var iRepo = new IndicatorsRepository(uof.Object);

            //Act
            var result = iRepo.Single(i => i.Name.Equals("indicator 1", StringComparison.InvariantCultureIgnoreCase), i => i.Measures);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Name.Equals("indicator 1", StringComparison.InvariantCultureIgnoreCase));
            Assert.IsNotNull(result.Measures);
            Assert.AreEqual(result.Measures.Count(), 1);
        }

        [Test]
        public void Can_Add_New()
        {
            //Arrange
            var iRepo = new IndicatorsRepository(uof.Object);

            //Act
            iRepo.Add(
                new Indicator()
                {
                    Id = new Guid(),
                    Name = "Indicator 5",
                    Code = "002"
                }
            );

            //Assert
            dbSet.Verify(s => s.Add(It.IsAny<Indicator>()), Times.Once);
        }

        [Test]
        public void Can_Update_Existing()
        {
            //Arrange
            var iRepo = new IndicatorsRepository(uof.Object);

            //Act
            iRepo.Update(
                new Indicator()
                {
                    Id = new Guid(),
                    Name = "Indicator 5",
                    Code = "002"
                }
            );

            //Assert
            dbSet.Verify(s => s.Attach(It.IsAny<Indicator>()), Times.Once);
            dbContext.Verify(s => s.SetState(It.IsAny<Indicator>(), EntityState.Modified), Times.Once);
        }

        [Test]
        public void Can_Remove_Existing()
        {
            //Arrange
            var iRepo = new IndicatorsRepository(uof.Object);

            //Act
            iRepo.Delete(indicatorList.First().Id);

            //Assert
            dbSet.Verify(s => s.Remove(It.IsAny<Indicator>()), Times.Once);
        }

        [Test]
        public void Can_Add_New_Measure()
        {
            //Arrange
            var iRepo = new IndicatorsRepository(uof.Object);
            var measure = new IndicatorMeasure()
            {
                Date = new DateTime(),
                Id = indicatorList.First().Measures.First().Id,
                IndicatorId = indicatorList.First().Measures.First().IndicatorId,
                RealValue = "20",
                TargetValue = "32"
            };

            //Act
            iRepo.AddMeasure(measure);

            //Assert
            dbContext.Verify(s => s.SetState(It.IsAny<IndicatorMeasure>(), EntityState.Added));
        }

        [Test]
        public void Can_Update_Measure()
        {
            //Arrange
            var iRepo = new IndicatorsRepository(uof.Object);
            var measure = new IndicatorMeasure()
            {
                Date = new DateTime(),
                Id = indicatorList.First().Measures.First().Id,
                IndicatorId = indicatorList.First().Measures.First().IndicatorId,
                RealValue = "20",
                TargetValue = "32"
            };

            //Act
            iRepo.UpdateMeasure(measure);

            //Assert
            dbContext.Verify(s => s.SetValues(indicatorList.First().Measures.First(), measure));
            dbContext.Verify(s => s.SetState(It.IsAny<IndicatorMeasure>(), EntityState.Modified));
        }

        [Test]
        public void Can_Delete_Measure()
        {
            //Arrange
            var iRepo = new IndicatorsRepository(uof.Object);

            //Act
            iRepo.DeleteMeasure(indicatorList.First().Id, indicatorList.First().Measures.First().Id);

            //Assert
            dbContext.Verify(s => s.SetState(It.IsAny<IndicatorMeasure>(), EntityState.Deleted));
        }
    }
}
