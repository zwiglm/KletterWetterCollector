using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BaseService.App_Start
{
    public class KwLog4NetManager
    {
        public static void InitializeLog4Net(HttpServerUtility server)
        {
            DirectoryInfo di = new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory);
            string pathToFeLogs = server.MapPath("/logs");
            log4net.GlobalContext.Properties["FeLogPath"] = pathToFeLogs;
            log4net.GlobalContext.Properties["LogMachine"] = server.MachineName;
            log4net.GlobalContext.Properties["LogHost"] = di.Name;

            //initialize the log4net configuration based on the log4net.config file
            XmlConfigurator.ConfigureAndWatch(new FileInfo(System.AppDomain.CurrentDomain.BaseDirectory + @"\KwLog4Net.config"));

        }
    }
}