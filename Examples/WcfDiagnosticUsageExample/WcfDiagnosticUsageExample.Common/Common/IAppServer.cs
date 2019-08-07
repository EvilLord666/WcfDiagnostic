using System;
using System.Collections.Generic;
using System.ServiceModel;
using Wcf.Diagnostics.Core.Data;

namespace WcfDiagnosticUsageExample.Common.Common
{
    [ServiceContract(CallbackContract = typeof(IAppCallback))]
    public interface IAppServer
    {
        [OperationContract]
        bool Login(string userName, string password);
        
        [OperationContract]
        bool Logout();
        
        IList<LogInfo> GetLogFiles(string clientId);
        string GetLogFile(string clientId, string fileName);
        string GetVersion(string clientId);
        IList<string> GetEnvironmentVariables(string clientId);
    }
}