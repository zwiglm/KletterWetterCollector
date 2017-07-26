using BaseService.DataAccess.ApiCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseService.DataAccess.ApiCommands
{
    public class KwWeatherDataCmd : BaseCommand
    {
        public String coreId;
        public String userId;
        public DateTime publishedAt;
        public String prtclEvent;
        public int fwVersion;

        public float temperature;
        public float humidityRh;
        public float pressure;
        public float rainMM;
        public float windKPH;
        public float gustKPH;
        public float windDirection;
        public float powerStatus;

        public KwWeatherDataCmd(String coreId, DateTime publishedAt, String userId, String prtclEvent, int fwVersion,
                             float temperature, float humidityRh, float pressure, float rainMM, float windKPH, float gustKPH, float windDirection, float powerStatus)
        {
            this.coreId = coreId;
            this.userId = userId;
            this.publishedAt = publishedAt;
            this.prtclEvent = prtclEvent;
            this.fwVersion = fwVersion;

            this.temperature = temperature;
            this.humidityRh = humidityRh;
            this.pressure = pressure;
            this.rainMM = rainMM;
            this.windKPH = windKPH;
            this.gustKPH = gustKPH;
            this.windDirection = windDirection;
            this.powerStatus = powerStatus;
        }

    }
}
