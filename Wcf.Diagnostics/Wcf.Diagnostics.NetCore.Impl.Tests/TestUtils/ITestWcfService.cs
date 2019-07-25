using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using Wcf.Diagnostics.Core.Interfaces;

namespace Wcf.Diagnostics.NetCore.Impl.Tests.TestUtils
{
    [ServiceContract(CallbackContract = typeof(ILogsCaptureCallback))]
    public interface ITestWcfService
    {
        [OperationContract]
        int LogIn(string domain, string login, string password);

        [OperationContract]
        bool LogOut(int sessionId);
    }
}
