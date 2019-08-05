using System;
using System.Collections.Generic;
using WcfDiagnosticUsageExample.Common.Common;

namespace WcfDiagnosticUsageExample.Server
{
    public class AppServer : IAppServer
    {
        public bool Login(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public bool Logout()
        {
            throw new NotImplementedException();
        }

        public IList<string> GetLogFiles<T>(T clientId) where T : IComparable
        {
            throw new NotImplementedException();
        }

        public string GetLogFile<T>(T clientId) where T : IComparable
        {
            throw new NotImplementedException();
        }

        public string GetVersion<T>(T clientId) where T : IComparable
        {
            throw new NotImplementedException();
        }

        public IList<string> GetEnvironmentVariables<T>(T clientId) where T : IComparable
        {
            throw new NotImplementedException();
        }
    }
}