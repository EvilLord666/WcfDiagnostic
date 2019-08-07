using System;
using System.Collections.Generic;
using System.ServiceModel;
using Wcf.Diagnostics.Core.Data;
using Wcf.Diagnostics.Core.Interfaces;

namespace WcfDiagnosticUsageExample.Common.Common
{
    [ServiceContract(CallbackContract = typeof(ILogsCaptureCallback))]
    public interface IAppServerBase
    {
        IList<LogInfo> GetLogFiles(string clientId);
        string GetLogFile(string clientId, string fileName);

        //[OperationContract]
        //void Empty();
    }

    [ServiceContract(CallbackContract = typeof(IAppCallback))]
    public interface IAppServer : IAppServerBase
    {
        [OperationContract]
        bool Login(string userName, string password);
        
        [OperationContract]
        bool Logout();
        
        string GetVersion(string clientId);
        IList<string> GetEnvironmentVariables(string clientId);
    }
}