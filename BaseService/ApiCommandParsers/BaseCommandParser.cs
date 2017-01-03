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
                //{
                //    "cmd": "updBaseData",
                //    "temperatur": 24.3,
                //    "pwrWarn": 0
                //}
                case "updBaseData":
                    {
                        DateTime now = DateTime.Now;
                        decimal temp = container.GetPropertyValue("temperatur").Value<decimal>();
                        short pwrWarn = container.GetPropertyValue("pwrWarn").Value<short>();

                        return new UpdBaseDataCmd(now, temp, pwrWarn);
                    }
                default:
                    throw new FailWithFeedbackException("Command not recognized");
            }
        }
    }
}
