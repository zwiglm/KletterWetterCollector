using BaseService.Application.ApiCommandHandlers;
using BaseService.Configuration;
using BaseService.Controllers;
using BaseService.DataAccess;
using BaseService.DataAccess.ApiCommandParsers;
using BaseService.DataAccess.ApiCommands;
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
                    // Handlers
                    RegisterCommandHandlers(x);

                    // Configuration
                    x.For<IDatabaseConfiguration>().Use<WebConfiguration>();
                    x.For<IApplicationConfiguration>().Use<WebConfiguration>();

                    // Parsers
                    x.For<BaseCommandsController>().Use<BaseCommandsController>().Ctor<ICommandParser>("commandParser").Is<BaseCommandParser>();

                    // Services

                    // ReadStores
                });

            // Make ASP.NET MVC delegate the task of instantiating controllers and dependencies to StructureMap.
            config.Services.Replace(typeof(IHttpControllerActivator), new MpServiceActivator(config));

            return ObjectFactory.Container;
        }

        private static void RegisterCommandHandlers(IInitializationExpression ie)
        {
            ie.Scan(s =>
            {
                s.AssemblyContainingType<AbstractCommand>();
                s.ConnectImplementationsToTypesClosing(typeof(IApiCmdHandle<>));
            });
        }

    }
}