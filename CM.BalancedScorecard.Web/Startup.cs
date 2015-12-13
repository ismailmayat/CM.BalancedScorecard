using CM.BalancedScorecard.Services.Mapper;
using Microsoft.Owin;
using Newtonsoft.Json;
using Owin;

[assembly: OwinStartup(typeof(CM.BalancedScorecard.Web.Startup))]

namespace CM.BalancedScorecard.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
            
            Mappings.Configure();
            log4net.Config.XmlConfigurator.Configure();
        }
    }
}
