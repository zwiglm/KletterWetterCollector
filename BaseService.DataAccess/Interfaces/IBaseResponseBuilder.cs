using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseService.ApiResponseBuilders
{
    public interface IBaseResponseBuilder
    {
        object FailResponse(string message);
        object BaseDataSuccessResponse(int rowsInserted);
    }
}
