using System.Collections.Generic;
using System.ServiceModel;
using Wcf.Diagnostics.Core.Interfaces;

namespace WcfDiagnosticUsageExample.Common.Common
{
    [ServiceContract]
    public interface IAppCallback : ILogsCaptureCallback
    {
        [OperationContract]
        string GetVersion();

        [OperationContract]
        IList<string> GetEnvironmentDescription();
    }
}