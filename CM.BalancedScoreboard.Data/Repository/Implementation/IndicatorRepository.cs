using CM.BalancedScoreboard.Data.Repository.Abstract;
using CM.BalancedScoreboard.Domain.Model.Indicators;

namespace CM.BalancedScoreboard.Data.Repository.Implementation
{
    public class IndicatorRepository : BaseRepository<Indicator>, IIndicatorRepository
    {
        public IndicatorRepository(IUnitOfWork uof) : base(uof) { }
    }   
}
