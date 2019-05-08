using System;
using System.Collections.Generic;
using System.Text;

namespace Wcf.Diagnostics.NetCore.Impl.Tests.TestUtils
{
    public class TestWcfService : ITestWcfService
    {
        public int LogIn(string domain, string login, string password)
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            return rand.Next(1000, Int32.MaxValue);
        }

        public bool LogOut(int sessionId)
        {
            return true;
        }
    }
}
