using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.Threading.Tasks;
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

        [OperationContract]
        Task<string> GetLogFileAsync(string fileName);
    }
}
