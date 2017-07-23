using BaseService.DataAccess.ApiCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseService.Interfaces
{
    public interface IBaseDao
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseCommand"></param>
        /// <returns>number of inserted rows</returns>
        int insertBaseDataCmd(UpdBaseDataCmd baseCommand);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="weatherData"></param>
        /// <returns></returns>
        int insertWeatheData(KwWeatherDataCmd weatherData);
    }
}
