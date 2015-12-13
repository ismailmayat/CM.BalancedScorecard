using CM.BalancedScoreboard.Data.Repository.Abstract;
using CM.BalancedScoreboard.Domain.Model.Indicators;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CM.BalancedScoreboard.Data.Tests
{
    public static class Shared
    {
        public static IEnumerable<Indicator> GetIndicatorList()
        {
            var indicators = new List<Indicator>()
            {
                new Indicator()
                {
                    Id = Guid.NewGuid(),
                    Name = "Indicator 1",
                    Code = "001"
                },
                new Indicator()
                {
                    Id = Guid.NewGuid(),
                    Name = "Indicator 2",
                    Code = "002"
                },
                new Indicator()
                {
                    Id = Guid.NewGuid(),
                    Name = "Indicator 3",
                    Code = "003"
                },
                new Indicator()
                {
                    Id = Guid.NewGuid(),
                    Name = "Indicator 4",
                    Code = "004"
                }
            };

            var indicatorMeasures = new List<IndicatorMeasure>()
            {
                new IndicatorMeasure()
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Today,
                    RealValue = "20",
                    TargetValue = "14"
                },
                new IndicatorMeasure()
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Today,
                    RealValue = "15",
                    TargetValue = "16"
                },
                new IndicatorMeasure()
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Today,
                    RealValue = "9",
                    TargetValue = "114"
                },
                new IndicatorMeasure()
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Today,
                    RealValue = "21",
                    TargetValue = "84"
                }
            };

            for (int index = 0; index < indicators.Count; index++)
            {
                indicatorMeasures[index].IndicatorId = indicators[index].Id;
                indicators[index].Measures = new List<IndicatorMeasure>() { indicatorMeasures[index] };       
            }

            return indicators;
        }

        public static Mock<IDbSet<TEntity>> GetDbSetTest<TEntity>(IEnumerable<TEntity> data) where TEntity : class
        {
            var queryable = data.AsQueryable();
            var dbSet = new Mock<IDbSet<TEntity>>();

            dbSet.Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());

            return dbSet;
        }

        public static Mock<IDbContext> GetDbContextTest<TEntity>(IDbSet<TEntity> testDbSet) where TEntity : class
        {
            var dbContext = new Mock<IDbContext>();
            dbContext.Setup(c => c.Set<TEntity>()).Returns(testDbSet);

            return dbContext;
        }

        public static Mock<IUnitOfWork> GetUnitfOfWorkTest(IDbContext testDbContext)
        {
            var uof = new Mock<IUnitOfWork>();
            uof.Setup(u => u.Context).Returns(testDbContext);

            return uof;
        }
    }
}
