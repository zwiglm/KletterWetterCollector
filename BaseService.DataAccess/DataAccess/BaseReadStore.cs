using BaseService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseService.DataAccess.Implementation
{
    public class BaseReadStore : IBaseReadStore
    {
        private IDatabaseConfiguration _databaseConfig;

        public BaseReadStore(IApplicationConfiguration appConfig, IDatabaseConfiguration databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }
    }
}
