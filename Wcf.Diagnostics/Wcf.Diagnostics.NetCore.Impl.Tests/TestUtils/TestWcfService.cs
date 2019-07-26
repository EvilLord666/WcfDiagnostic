using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using Wcf.Diagnostics.Core.Data;
using Wcf.Diagnostics.Core.Interfaces;

namespace Wcf.Diagnostics.NetCore.Impl.Tests.TestUtils
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class TestWcfService : ITestWcfService
    {
        public int LogIn(string domain, string login, string password)
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            return rand.Next(0, 1000);
        }

        public bool LogOut(int sessionId)
        {
            return true;
        }

        public IList<LogInfo> GetClientLogs()
        {
            ILogsCaptureCallback clientChannel = GetClientCallbackChannel();
            IList<LogInfo> result = clientChannel.GetLogsFiles();
            return result;
        }

        private ILogsCaptureCallback GetClientCallbackChannel()
        {
            return OperationContext.Current.GetCallbackChannel<ILogsCaptureCallback>();
        }
    }
}
