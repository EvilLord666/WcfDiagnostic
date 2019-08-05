using System;
using System.Collections.Generic;
using WcfDiagnosticUsageExample.Common.Common;

namespace WcfDiagnosticUsageExample.Server
{
    public class AppServer<T> : IAppServer<T>
        where T: IComparable
    {
        public bool Login(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public bool Logout()
        {
            throw new NotImplementedException();
        }

        public IList<string> GetLogFiles(T clientId)
        {
            throw new NotImplementedException();
        }

        public string GetLogFile(T clientId)
        {
            throw new NotImplementedException();
        }

        public string GetVersion(T clientId)
        {
            throw new NotImplementedException();
        }

        public IList<string> GetEnvironmentVariables(T clientId) 
        {
            throw new NotImplementedException();
        }
        
        //public event 
        
    }
}