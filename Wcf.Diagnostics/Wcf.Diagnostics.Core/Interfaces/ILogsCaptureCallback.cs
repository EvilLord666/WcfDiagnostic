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
        [OperationContract(Name = "GetLogsFiles")]
        IList<LogInfo> GetLogsFiles();

        [OperationContract(Name = "GetLogFile")]
        string GetLogFile(string fileName);

        [OperationContract(Name = "GetLogFileAsync")]
        Task<string> GetLogFileAsync(string fileName);
    }
}
