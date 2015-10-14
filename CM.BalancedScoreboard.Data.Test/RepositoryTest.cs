using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CM.BalancedScoreboard.Data.Repository.Abstract;
using CM.BalancedScoreboard.Data.Repository.Implementation;
using CM.BalancedScoreboard.Domain.Model.Indicators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CM.BalancedScoreboard.Data.Tests
{
    [TestClass]
    public class RepositoryTest
    {
        [TestMethod]
        public void Can_Return_All()
        {
            //Arrange
            var dbSet = GetDbSetTest<Indicator>(GetIndicatorList());
            var dbContext = GetDbContextTest<Indicator>(dbSet.Object);
            var uof = GetUnitfOfWorkTest(dbContext.Object);
            var iRepo = new IndicatorRepository(uof.Object);

            //Act
            var result = iRepo.Get();

            //Assert
            Assert.AreEqual(result.Count(), 4);
        }

        [TestMethod]
        public void Can_Return_Filtered()
        {
            //Arrange
            var dbSet = GetDbSetTest<Indicator>(GetIndicatorList());
            var dbContext = GetDbContextTest<Indicator>(dbSet.Object);
            var uof = GetUnitfOfWorkTest(dbContext.Object);
            var iRepo = new IndicatorRepository(uof.Object);

            //Act
            var result = iRepo.Get(i => i.Code.Equals("000", StringComparison.InvariantCultureIgnoreCase));

            //Assert
            Assert.AreEqual(result.Count(), 2);
            Assert.IsTrue(result.ToList()[0].Code.Equals("000", StringComparison.InvariantCultureIgnoreCase));
            Assert.IsTrue(result.ToList()[1].Code.Equals("000", StringComparison.InvariantCultureIgnoreCase));
        }

        [TestMethod]
        public void Can_Return_Single()
        {
            //Arrange
            var dbSet = GetDbSetTest<Indicator>(GetIndicatorList());
            var dbContext = GetDbContextTest<Indicator>(dbSet.Object);
            var uof = GetUnitfOfWorkTest(dbContext.Object);
            var iRepo = new IndicatorRepository(uof.Object);

            //Act
            var result = iRepo.Single(i => i.Name.Equals("indicator 1", StringComparison.InvariantCultureIgnoreCase));

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Name.Equals("indicator 1", StringComparison.InvariantCultureIgnoreCase));
        }

        [TestMethod]
        public void Can_Return_Single_With_Navigation_Property()
        {
            //Arrange
            var dbSet = GetDbSetTest<Indicator>(GetIndicatorList());
            var dbContext = GetDbContextTest<Indicator>(dbSet.Object);
            var uof = GetUnitfOfWorkTest(dbContext.Object);
            var iRepo = new IndicatorRepository(uof.Object);

            //Act
            var result = iRepo.Single(i => i.Name.Equals("indicator 1", StringComparison.InvariantCultureIgnoreCase), i => i.Values);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Name.Equals("indicator 1", StringComparison.InvariantCultureIgnoreCase));
            Assert.IsNotNull(result.Values);
            Assert.AreEqual(result.Values.Count(), 1);
        }

        [TestMethod]
        public void Can_Add_New()
        {
            //Arrange
            var dbSet = GetDbSetTest<Indicator>(GetIndicatorList());
            var dbContext = GetDbContextTest<Indicator>(dbSet.Object);
            var uof = GetUnitfOfWorkTest(dbContext.Object);
            var iRepo = new IndicatorRepository(uof.Object);

            //Act
            iRepo.Add(new List<Indicator>()
            {
                new Indicator()
                {
                    Id = new Guid(),
                    Name = "Indicator 5",
                    Code = "002"
                }
            });

            //Assert
            dbSet.Verify(s => s.Add(It.IsAny<Indicator>()), Times.Once);
        }

        [TestMethod]
        public void Can_Update_Existing()
        {
            //Arrange
            var dbSet = GetDbSetTest<Indicator>(GetIndicatorList());
            var dbContext = GetDbContextTest<Indicator>(dbSet.Object);
            var uof = GetUnitfOfWorkTest(dbContext.Object);
            var iRepo = new IndicatorRepository(uof.Object);

            //Act
            iRepo.Update(new List<Indicator>()
            {
                new Indicator()
                {
                    Id = new Guid(),
                    Name = "Indicator 5",
                    Code = "002"
                }
            });

            //Assert
            dbSet.Verify(s => s.Attach(It.IsAny<Indicator>()), Times.Once);
            dbContext.Verify(s => s.SetModified(It.IsAny<Indicator>()), Times.Once);
        }

        [TestMethod]
        public void Can_Remove_Existing()
        {
            //Arrange
            var dbSet = GetDbSetTest<Indicator>(GetIndicatorList());
            var dbContext = GetDbContextTest<Indicator>(dbSet.Object);
            var uof = GetUnitfOfWorkTest(dbContext.Object);
            var iRepo = new IndicatorRepository(uof.Object);

            //Act
            iRepo.Delete(new List<Indicator>()
            {
                new Indicator()
                {
                    Id = new Guid(),
                    Name = "Indicator 5",
                    Code = "002"
                }
            });

            //Assert
            dbSet.Verify(s => s.Remove(It.IsAny<Indicator>()), Times.Once);
        }

        private static Mock<IDbSet<TEntity>> GetDbSetTest<TEntity>(IEnumerable<TEntity> data) where TEntity : class
        {
            var queryable = data.AsQueryable();
            var dbSet = new Mock<IDbSet<TEntity>>();

            dbSet.Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());

            return dbSet;
        }

        private static Mock<IDbContext> GetDbContextTest<TEntity>(IDbSet<TEntity> testDbSet) where TEntity : class
        {
            var dbContext = new Mock<IDbContext>();
            dbContext.Setup(c => c.Set<TEntity>()).Returns(testDbSet);

            return dbContext;
        }

        private static Mock<IUnitOfWork> GetUnitfOfWorkTest(IDbContext testDbContext)
        {
            var uof = new Mock<IUnitOfWork>();
            uof.Setup(u => u.Context).Returns(testDbContext);

            return uof;
        }

        private static IEnumerable<Indicator> GetIndicatorList()
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
            };
        } 
    }
}
