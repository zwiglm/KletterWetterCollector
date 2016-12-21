using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseService.DataAccess.Exceptions
{
    public class FailWithFeedbackException : Exception
    {
        public FailWithFeedbackException(string feedback)
            : base(feedback) { }

        public FailWithFeedbackException(string feedback, Exception ex)
            : base(feedback, ex) { }
    }
}
