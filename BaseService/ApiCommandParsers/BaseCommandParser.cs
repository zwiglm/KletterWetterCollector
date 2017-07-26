using BaseService.DataAccess.ApiCommands;
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
using System.Globalization;


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
                        String prtclEvent = container.GetPropertyValue("event").Value<String>();

                        String data = container.GetPropertyValue("data").Value<String>();
                        string[] dataPairs = data.Replace("{", "").Replace("}", "").Split(',');

                        float temperature = float.Parse((dataPairs[0].Split(':'))[1].Replace("\"", ""), CultureInfo.InvariantCulture.NumberFormat);
                        float humidityRh = float.Parse((dataPairs[1].Split(':'))[1].Replace("\"", ""), CultureInfo.InvariantCulture.NumberFormat);
                        float pressure = float.Parse((dataPairs[2].Split(':'))[1].Replace("\"", ""), CultureInfo.InvariantCulture.NumberFormat);
                        float rainMM = float.Parse((dataPairs[3].Split(':'))[1].Replace("\"", ""), CultureInfo.InvariantCulture.NumberFormat);
                        float windKPH = float.Parse((dataPairs[4].Split(':'))[1].Replace("\"", ""), CultureInfo.InvariantCulture.NumberFormat);
                        float gustKPH = float.Parse((dataPairs[5].Split(':'))[1].Replace("\"", ""), CultureInfo.InvariantCulture.NumberFormat);
                        float windDirection = float.Parse((dataPairs[6].Split(':'))[1].Replace("\"", ""), CultureInfo.InvariantCulture.NumberFormat);
                        float powerStatus = float.Parse((dataPairs[7].Split(':'))[1].Replace("\"", ""), CultureInfo.InvariantCulture.NumberFormat);

                        return new KwWeatherDataCmd(coreId, publishedAt, data, prtclEvent,
                                                    temperature, humidityRh, pressure, rainMM, windKPH, gustKPH, windDirection, powerStatus);
                    }
                default:
                    throw new FailWithFeedbackException("Command not recognized");
            }
        }


    }
}
