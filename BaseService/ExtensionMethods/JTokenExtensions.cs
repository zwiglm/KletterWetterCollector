using BaseService.DataAccess.Exceptions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseService.ExtensionMethods
{
    public static class JTokenExtensions
    {
        public static JToken GetPropertyValue(this JToken token, string propertyName)
        {
            try
            {
                JProperty commandProperty = token.Where(e => (e as JProperty).Name == propertyName)
                                                 .FirstOrDefault() as JProperty;

                return commandProperty.Value as JToken;
            }
            catch (Exception ex)
            {
                throw new FailWithFeedbackException(String.Format("Unable to parse property '{0}'", propertyName));
            }
        }
    }
}