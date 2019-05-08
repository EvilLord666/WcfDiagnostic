using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;

namespace Wcf.Diagnostics.NetCore.Impl.Tests.TestUtils
{
    [ServiceContract]
    public interface ITestWcfService
    {
        [OperationContract]
        int LogIn(string domain, string login, string password);

        [OperationContract]
        bool LogOut(int sessionId);
    }
}
