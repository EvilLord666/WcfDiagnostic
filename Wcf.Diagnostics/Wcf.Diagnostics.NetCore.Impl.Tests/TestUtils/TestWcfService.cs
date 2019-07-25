using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;

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
    }
}
