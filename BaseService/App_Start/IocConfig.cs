using BaseService.Configuration;
using BaseService.DataAccess;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace BaseService.App_Start
{
    public static class IocConfig
    {
        public static IContainer Register(HttpConfiguration config)
        {
            ObjectFactory.Initialize(x =>
                {
                    // Configuration
                    x.For<IDatabaseConfiguration>().Use<WebConfiguration>();
                    x.For<IApplicationConfiguration>().Use<WebConfiguration>();

                });

            // Make ASP.NET MVC delegate the task of instantiating controllers and dependencies to StructureMap.
            config.Services.Replace(typeof(IHttpControllerActivator), new MpServiceActivator(config));

            return ObjectFactory.Container;
        }
    }
}