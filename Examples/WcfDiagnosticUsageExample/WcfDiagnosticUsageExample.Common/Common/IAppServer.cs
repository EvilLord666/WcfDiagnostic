using System;
using System.Collections.Generic;
using System.ServiceModel;
using Wcf.Diagnostics.Core.Interfaces;

namespace WcfDiagnosticUsageExample.Common.Common
{
    [ServiceContract(CallbackContract = typeof(IAppCallback))]
    public interface IAppServer<T> where T : IComparable
    {
        [OperationContract]
        bool Login(string userName, string password);
        
        [OperationContract]
        bool Logout();
        
        IList<string> GetLogFiles(T clientId);
        string GetLogFile(T clientId);
        string GetVersion(T clientId);
        IList<string> GetEnvironmentVariables(T clientId);
    }
}