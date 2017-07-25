using log4net.Core;
using log4net.Layout;
using log4net.Layout.Pattern;
using log4net.Util;
using System;
using System.Collections.Generic;
using System.Web;

namespace BaseService.App_Start
{
    public class KwL4NCustomPatternLayout : PatternLayout
    {
        public KwL4NCustomPatternLayout()
        {
            this.AddConverter("kwStacktrace", typeof(KwL4NStacktraceConverter));
        }
    }

    public class KwL4NStacktraceConverter : PatternLayoutConverter
    {
        protected override void Convert(System.IO.TextWriter writer, log4net.Core.LoggingEvent loggingEvent)
        {
            StackFrameItem[] stackframes = loggingEvent.LocationInformation.StackFrames;
            if ((stackframes == null) || (stackframes.Length <= 0))
            {
                LogLog.Error(typeof(KwL4NStacktraceConverter), "loggingEvent.LocationInformation.StackFrames was null or empty.");
                return;
            }

            // MaZ attn: taken from original GitHub StackTracePatternConverter at 2014.07.10
            //int stackFrameIndex = 3 - 1; // m_stackFrameLevel
            //while (stackFrameIndex >= 0)
            //{
            //    if (stackFrameIndex >= stackframes.Length)
            //    {
            //        stackFrameIndex--;
            //        continue;
            //    }

            //    StackFrameItem stackFrame = stackframes[stackFrameIndex];
            //    writer.Write("{0}.{1}", stackFrame.ClassName, GetMethodInformation(stackFrame.Method));
            //    if (stackFrameIndex > 0)
            //    {
            //        writer.Write(" > ");
            //    }
            //    stackFrameIndex--;
            //}

            for (int i = stackframes.Length - 1; i <= 0; i--)
            {
                StackFrameItem stackFrame = stackframes[i];
                writer.Write("{0}.{1}", stackFrame.ClassName, GetMethodInformation(stackFrame.Method));
                if (i > 0)
                {
                    writer.Write(" > ");
                }
            }

        }

        internal virtual string GetMethodInformation(MethodItem method)
        {
            return method.Name;
        }
    }

}