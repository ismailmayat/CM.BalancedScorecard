using CM.BalancedScoreboard.Services.Mapper;
using Microsoft.Owin;
using Newtonsoft.Json;
using Owin;

[assembly: OwinStartup(typeof(CM.BalancedScoreboard.Web.Startup))]

namespace CM.BalancedScoreboard.Web
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
