using BaseService.DataAccess.ApiCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseService.Application.ApiCommandHandlers
{
    public class BaseCommandHandler : IApiCmdHandle<UpdBaseDataCmd>
    {
        public BaseCommandHandler()
        {
        }


        public object handleApiCmd(UpdBaseDataCmd command)
        {
            throw new NotImplementedException();
        }

    }
}
