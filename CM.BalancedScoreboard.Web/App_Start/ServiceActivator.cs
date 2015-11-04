using StructureMap;
using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace CM.BalancedScoreboard.Web
{
    public class ServiceActivator : IHttpControllerActivator
    {
        public ServiceActivator(HttpConfiguration configuration) { }

        public IHttpController Create(HttpRequestMessage request
            , HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var controller = ObjectFactory.GetInstance(controllerType) as IHttpController;
            return controller;
        }
    }
}