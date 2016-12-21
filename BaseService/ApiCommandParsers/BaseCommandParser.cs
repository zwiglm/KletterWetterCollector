using BaseService.DataAccess.ApiCommands;
using BaseService.DataAccess.Exceptions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseService.DataAccess.ApiCommandParsers
{
    public class BaseCommandParser : ICommandParser
    {
        public AbstractCommand parseCommand(object data)
        {
            throw new NotImplementedException();
        }


        private AbstractCommand parseCommand(string commandName, JContainer container)
        {
            switch (commandName.ToLower())
            {
                case "updBaseData":
                    {
                        DateTime now = DateTime.Now;
                        decimal temp = new Decimal(24.3);
                        short pwrWarn = 0;

                        return new UpdBaseDataCmd(now, temp, pwrWarn);
                    }
                default:
                    throw new FailWithFeedbackException("Command not recognized");
            }
        }
    }
}
