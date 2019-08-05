using System;
using System.Collections.Generic;
using System.ServiceModel;
using Wcf.Diagnostics.Core.Interfaces;

namespace WcfDiagnosticUsageExample.Common.Common
{
    [ServiceContract(CallbackContract = typeof(IAppCallback))]
    public interface IAppServer
    {
        [OperationContract]
        bool Login(string userName, string password);
        
        [OperationContract]
        bool Logout();
        
        IList<string> GetLogFiles<T>(T clientId) where T : IComparable;
        string GetLogFile<T>(T clientId) where T : IComparable;
        string GetVersion<T>(T clientId) where T : IComparable;
        IList<string> GetEnvironmentVariables<T>(T clientId) where T : IComparable;
    }
}