using CM.BalancedScorecard.Data.Repository.Abstract;
using CM.BalancedScorecard.Data.Repository.Abstract.Indicators;
using CM.BalancedScorecard.Data.Tests;
using CM.BalancedScorecard.Domain.Model.Indicators;
using CM.BalancedScorecard.Domain.Model.Users;
using CM.BalancedScorecard.Resources.Abstract;
using CM.BalancedScorecard.Services.Abstract.Indicators;
using CM.BalancedScorecard.Services.Implementation.Indicators;
using CM.BalancedScorecard.Services.ViewModel.Indicators;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace CM.BalancedScorecard.Services.Tests.Indicators
{
    [TestFixture]
    public class ServiceTest
    {
        Mock<IIndicatorsRepository> iRepo;
        Mock<IBaseRepository<IndicatorType>> itRepo;
        Mock<IBaseRepository<User>> uRepo;
        Mock<IIndicatorViewModelFactory> ivmFactory;
        Mock<IResourceFactory> irFactory;

        [SetUp]
        public void Initialize()
        {
            iRepo = new Mock<IIndicatorsRepository>();
            itRepo = new Mock<IBaseRepository<IndicatorType>>();
            uRepo = new Mock<IBaseRepository<User>>();
            ivmFactory = new Mock<IIndicatorViewModelFactory>();
            irFactory = new Mock<IResourceFactory>();
        }

        [Test]
        public void Can_Apply_Filter()
        {
            //Arrange
            var filter = "Indicator";
            var service = new IndicatorsService(iRepo.Object, itRepo.Object, uRepo.Object, ivmFactory.Object, irFactory.Object);

            //Act
            var indicators = service.ApplyIndicatorsFilter(Shared.GetIndicatorList().AsQueryable(), filter);

            //Arrange
            Assert.IsInstanceOf<IQueryable<Indicator>>(indicators);
            Assert.AreEqual(indicators.ToList().Count, 4);
        }

        [Test]
        public void Can_Return_IndicatorList_ViewModel()
        {
            //Arrange
            iRepo.Setup(s => s.GetAll()).Returns(Shared.GetIndicatorList().AsQueryable());
            ivmFactory.Setup(s => s.CreateIndicatorListViewModel(It.IsAny<IQueryable<Indicator>>())).Returns(new IndicatorListViewModel());
            var filter = "Indicator";
            var service = new IndicatorsService(iRepo.Object, itRepo.Object, uRepo.Object, ivmFactory.Object, irFactory.Object);           

            //Act
            var result = service.GetIndicators(filter);

            //Assert
            ivmFactory.Verify(s => s.CreateIndicatorListViewModel(It.IsAny<IQueryable<Indicator>>()));
            Assert.IsNotNull(result);
        }

        //[Test]
        //public void Can_Return_And_Map_Indicator()
        //{
        //    //Arrange
        //    //var id = new Guid();
        //    //var repo = new Mock<IIndicatorRepository>();
        //    //repo.Setup(
        //    //    r =>
        //    //        r.Single(It.IsAny<Expression<Func<Indicator, bool>>>(),
        //    //            It.IsAny<Expression<Func<Indicator, object>>>())).Returns(GetIndicator(id));
        //    //var service = new IndicatorsService(repo.Object, );            

        //    ////Act
        //    //var result = service.GetIndicator(id);

        //    ////Assert
        //    //Assert.IsTrue(result.Indicator.Name == "Indicator 1");
        //   // Assert.IsTrue(result.Indicator.Measures.Count() == 1);
        //}

        //private static Indicator GetIndicator(Guid id)
        //{
        //    return new Indicator()
        //    {
        //        Id = id,
        //        Name = "Indicator 1",
        //        Code = "000",
        //        Measures = new List<IndicatorMeasure>()
        //        {
        //            new IndicatorMeasure()
        //            {
        //                Id = Guid.NewGuid(),
        //                Date = DateTime.Today,
        //                RealValue = "20",
        //                TargetValue = "14"
        //            }
        //        }
        //    };
        //}
    }
}
