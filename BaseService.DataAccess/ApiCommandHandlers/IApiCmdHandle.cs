using BaseService.DataAccess.ApiCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseService.Application.ApiCommandHandlers
{
    public interface IApiCmdHandle<TCommand> where TCommand : AbstractCommand
    {
        object handleApiCmd(TCommand command);
    }
}
