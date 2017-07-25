﻿using BaseService.DataAccess.ApiCommands;
using BaseService.DataAccess.Exceptions;
using BaseService.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using log4net;
using BaseService.Application;


namespace BaseService.DataAccess.ApiCommandParsers
{
    public class BaseCommandParser : ICommandParser
    {
        private ILog _logger;


        public BaseCommandParser()
        {
            _logger = LogManager.GetLogger(LoggerConst.MAIN_DBG_LOGGER);
        }


        public AbstractCommand parseCommand(object data)
        {
            JContainer container = data as JContainer;
            if (_logger.IsDebugEnabled)
            {
                _logger.Debug(String.Format("Incomming Json-Data: {0}", container.ToString()));
            }

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
                        now = DateTime.Parse("2010-08-20T15:00:00.123Z", null, System.Globalization.DateTimeStyles.RoundtripKind);

                        decimal temp = container.GetPropertyValue("temperatur").Value<decimal>();
                        short pwrWarn = container.GetPropertyValue("pwrWarn").Value<short>();

                        return new UpdBaseDataCmd(now, temp, pwrWarn);
                    }
                //{
                //  "event": "KlettWettWriteAllWD",
                //  "data": "{\"field1\":\"25.5\",\"field2\":\"43\",\"field3\":\"97.4\",\"field4\":\"0.0\",\"field5\":\"0.0\",\"field6\":\"0.0\",\"field7\":\"0\",\"field8\":\"4.0\",}",
                //  "published_at": "2017-07-25T17:57:40.877Z",
                //  "coreid": "32002b000951343334363138",
                //  "userid": "596e81faf92bae142d36278e",
                //  "fw_version": 1,
                //  "public": false,
                //  "elevation": "",
                //  "long": "",
                //  "lat": "",
                //  "cmd": "KwFullWd"
                //}
                case "KwFullWd":
                    {
                        String coreId = container.GetPropertyValue("coreid").Value<String>();
                        DateTime publishedAt = container.GetPropertyValue("published_at").Value<DateTime>();
                        String data = container.GetPropertyValue("data").Value<String>();
                        String prtclEvent = container.GetPropertyValue("event").Value<String>();

                        float temperature = container.GetPropertyValue("field1").Value<float>();
                        float humidityRh = container.GetPropertyValue("field2").Value<float>();
                        float pressure = container.GetPropertyValue("field3").Value<float>();
                        float rainMM = container.GetPropertyValue("field4").Value<float>();
                        float windKPH = container.GetPropertyValue("field5").Value<float>();
                        float gustKPH = container.GetPropertyValue("field6").Value<float>();
                        float windDirection = container.GetPropertyValue("field7").Value<float>();
                        float powerStatus = container.GetPropertyValue("field8").Value<float>();

                        return new KwWeatherDataCmd(coreId, publishedAt, data, prtclEvent,
                                                    temperature, humidityRh, pressure, rainMM, windKPH, gustKPH, windDirection, powerStatus);
                    }
                default:
                    throw new FailWithFeedbackException("Command not recognized");
            }
        }
    }
}
