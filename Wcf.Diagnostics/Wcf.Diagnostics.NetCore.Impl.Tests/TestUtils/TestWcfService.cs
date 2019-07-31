using System;
using System.Collections.Concurrent;
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
            ProcessClient();
            return rand.Next(0, 1000);
        }

        public bool LogOut(int sessionId)
        {
            ProcessClient();
            return true;
        }

        public IList<LogInfo> GetClientLogs(string clientId)
        {
            ILogsCaptureCallback clientChannel = Clients[clientId];
            IList<LogInfo> result = clientChannel.GetLogsFiles();
            return result;
        }

        public string GetLogFile(string clientId, string logFileName)
        {
            ILogsCaptureCallback clientChannel = Clients[clientId];
            string result = clientChannel.GetLogFile(logFileName);
            return result;
        }

        private void ProcessClient()
        {
            OperationContext context = OperationContext.Current;
            string key = context.SessionId ?? context.Channel.RemoteAddress.Uri.Host;
            Clients[key] = GetClientCallbackChannel(context);
        }

        private ILogsCaptureCallback GetClientCallbackChannel(OperationContext context)
        {
            return context.GetCallbackChannel<ILogsCaptureCallback>();
        }
        
        public static readonly IDictionary<string, ILogsCaptureCallback> Clients = new ConcurrentDictionary<string, ILogsCaptureCallback>();
    }
}
