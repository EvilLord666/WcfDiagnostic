using System;
using System.Collections.Generic;
using System.IO;
using Wcf.Diagnostics.Core.Data;
using Wcf.Diagnostics.Core.Interfaces;

namespace Wcf.Diagnostics.Impl.Interfaces
{
    public class SimpleLogsCaptureCallback : ILogsCaptureCallback
    {
        public SimpleLogsCaptureCallback(string logsRootDirectory, bool includeSubDirs, IList<string> logFileFilters)
        {
            if(string.IsNullOrEmpty(logsRootDirectory))
                throw new ArgumentNullException("logsRootDirectory");
            _logsRootDirectory = logsRootDirectory;
            _includeSubDirs = includeSubDirs;
            _logFileFilters = logFileFilters ?? new List<string>();
            if(_logFileFilters.Count == 0)
                _logFileFilters.Add("*");
        }

        public IList<LogInfo> GetLogsFiles()
        {
            if (!Directory.Exists(_logsRootDirectory))
                return null;
            IList<LogInfo> logInfoResult = new List<LogInfo>();

            return logInfoResult;
        }

        public string GetLogFile(string fileName)
        {
            throw new System.NotImplementedException();
        }

        private string _logsRootDirectory;
        private bool _includeSubDirs;
        private IList<string> _logFileFilters;
    }
}