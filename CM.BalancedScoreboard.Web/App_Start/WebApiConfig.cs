using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using CM.BalancedScoreboard.Data.Repository.Abstract;
using CM.BalancedScoreboard.Data.Repository.Implementation;
using CM.BalancedScoreboard.Domain.Model.Indicators;
using CM.BalancedScoreboard.Services.Abstract;
using CM.BalancedScoreboard.Services.Implementation;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.Unity;
using Newtonsoft.Json.Serialization;

namespace CM.BalancedScoreboard.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var container = new UnityContainer();
            container.RegisterType<IIndicatorService, IndicatorService>(new HierarchicalLifetimeManager());
            container.RegisterType<IBaseRepository<Indicator>, BaseRepository<Indicator>>(new HierarchicalLifetimeManager());
            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager());
            //container.RegisterType<IDbContext, BsContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IDbContext>(new InjectionFactory(d => new BsContext()));
            config.DependencyResolver = new UnityResolver(container);
        }
    }
}
