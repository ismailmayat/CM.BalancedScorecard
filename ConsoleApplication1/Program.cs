using CM.BalancedScoreboard.Data.Repository.Implementation;
using CM.BalancedScoreboard.Domain.Model.Indicators;
using CM.BalancedScoreboard.Services.Implementation;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var uof = new UnitOfWork(new BsContext());
            var rep = new BaseRepository<Indicator>(uof);

            var ser = new IndicatorService(rep);

            ser.GetIndicators("70");
        }
    }
}
