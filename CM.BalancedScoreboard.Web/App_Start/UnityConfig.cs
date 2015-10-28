using System;
using CM.BalancedScoreboard.Data.Repository.Abstract;
using CM.BalancedScoreboard.Data.Repository.Implementation;
using CM.BalancedScoreboard.Services.Abstract;
using CM.BalancedScoreboard.Services.Implementation;
using Microsoft.Practices.Unity;

namespace CM.BalancedScoreboard.Web
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            container.RegisterType<IIndicatorsService, IndicatorsService>(new HierarchicalLifetimeManager());
            container.RegisterType<IIndicatorRepository, IndicatorRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager());
            container.RegisterType<IDbContext>(new HierarchicalLifetimeManager(), new InjectionFactory(d => new BsContext()));
        }
    }
}
