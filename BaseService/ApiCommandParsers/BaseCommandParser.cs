using BaseService.DataAccess.ApiCommands;
using BaseService.DataAccess.Exceptions;
using BaseService.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BaseService.DataAccess.ApiCommandParsers
{
    public class BaseCommandParser : ICommandParser
    {
        public AbstractCommand parseCommand(object data)
        {
            JContainer container = data as JContainer;

            string commandName;
            try
            {
                commandName = container.GetPropertyValue("cmd").Value<string>();
            }
            catch (NullReferenceException)
            {
                throw new FailWithFeedbackException("No cmd property specified");
            }

            return parseCommand(commandName, container);
        }


        private AbstractCommand parseCommand(string commandName, JContainer container)
        {
            switch (commandName)
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
