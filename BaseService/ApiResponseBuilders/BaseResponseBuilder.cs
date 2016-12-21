using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseService.ApiResponseBuilders
{
    public class BaseResponseBuilder : IBaseResponseBuilder
    {
        public object FailResponse(string message)
        {
            return new
            {
                status = "fail",
                data = new
                {
                    fail = message
                }
            };
        }

        public object BaseDataSuccessResponse(int rowsInserted)
        {
            return new
            {
                status = "success",
                data = new
                {
                    rowsInserted = rowsInserted
                }
            };
        }
    }
}