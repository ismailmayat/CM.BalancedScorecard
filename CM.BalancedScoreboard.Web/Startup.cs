using CM.BalancedScoreboard.Services.Mapper;
using CM.BalancedScoreboard.Web.Filters.Exception;
using Microsoft.Owin;
using Owin;
using System.Web.Http;

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
            GlobalConfiguration.Configuration.Filters.Add(new CustomExceptionFilter());
        }
    }
}
