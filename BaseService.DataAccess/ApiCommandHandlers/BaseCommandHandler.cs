using BaseService.ApiResponseBuilders;
using BaseService.DataAccess.ApiCommands;
using BaseService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseService.Application.ApiCommandHandlers
{
    public class BaseCommandHandler : IApiCmdHandle<UpdBaseDataCmd>,
                                      IApiCmdHandle<KwWeatherDataCmd>
    {
        private IBaseDao _baseDao;
        private IBaseResponseBuilder _baseResponseBuilder;

        public BaseCommandHandler(IBaseDao baseDao, IBaseResponseBuilder baseResponseBuilder)
        {
            _baseDao = baseDao;
            _baseResponseBuilder = baseResponseBuilder;
        }


        public object handleApiCmd(UpdBaseDataCmd command)
        {
            int insertedRows =_baseDao.insertBaseDataCmd(command);
            if (insertedRows == 0)
                return _baseResponseBuilder.FailResponse("Could not write BaseDataCommand");
            else
                return _baseResponseBuilder.BaseDataSuccessResponse(insertedRows);
        }

        public object handleApiCmd(KwWeatherDataCmd command)
        {
            int insertedRows = _baseDao.insertWeatheData(command);
            if (insertedRows == 0)
                return _baseResponseBuilder.FailResponse("Could not write WeatherData to DB");
            else
                return _baseResponseBuilder.BaseDataSuccessResponse(insertedRows);
        }

    }
}
