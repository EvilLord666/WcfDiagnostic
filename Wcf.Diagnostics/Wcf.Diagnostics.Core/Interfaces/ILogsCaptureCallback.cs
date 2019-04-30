using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using Wcf.Diagnostics.Core.Data;


namespace Wcf.Diagnostics.Core.Interfaces
{
    [ServiceContract]
    public interface ILogsCaptureCallback
    {
        [OperationContract]
        IList<LogInfo> GetLogsFiles();

        [OperationContract]
        string GetLogFile(string fileName);
    }
}
