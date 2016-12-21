using BaseService.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace BaseService.Configuration
{
    public class WebConfiguration : IApplicationConfiguration, IDatabaseConfiguration
    {
        public string MainConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["mainDb"].ConnectionString; }
        }
    }
}