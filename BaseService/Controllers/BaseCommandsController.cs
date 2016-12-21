using BaseService.Application.ApiCommandHandlers;
using BaseService.DataAccess.ApiCommandParsers;
using BaseService.DataAccess.ApiCommands;
using BaseService.DataAccess.Exceptions;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace BaseService.Controllers
{
    public class BaseCommandsController : ApiController
    {
        private ICommandParser _commandParser;


        public BaseCommandsController(ICommandParser commandParser)
        {
            _commandParser = commandParser;
        }


        [HttpPost]
        public object Post(object post)
        {
            // We want to start a transaction here
            try
            {
                AbstractCommand command = _commandParser.parseCommand(post);

                return RunHandlerForCommand(command);
            }
            catch (FailWithFeedbackException fwfEx)
            {
                return fwfEx.Message;
                //return _responseBuilder.BuildFailResponse(ex.Message);
            }
            catch (Exception ex)
            {
                return ex.Message;
                //return _responseBuilder.BuildInternalServerErrorResponse();
            }

            throw new NotImplementedException();
        }


        #region Private

        private object RunHandlerForCommand(AbstractCommand command)
        {
            Type handlerType = typeof(IApiCmdHandle<>).MakeGenericType(command.GetType());

            object handler = ObjectFactory.GetInstance(handlerType);

            // Maz attn: CAREFULL - not type-safe here!!!
            MethodInfo handleMethod = handlerType.GetMethod("handleApiCmd");

            return handleMethod.Invoke(handler, new object[] { command });
        }

        #endregion

    }
}
