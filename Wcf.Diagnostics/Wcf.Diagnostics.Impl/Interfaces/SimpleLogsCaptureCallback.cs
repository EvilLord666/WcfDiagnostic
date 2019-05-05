using System;
using System.Collections.Generic;
using System.IO;
using Wcf.Diagnostics.Core.Data;
using Wcf.Diagnostics.Core.Interfaces;

namespace Wcf.Diagnostics.NetCore.Impl.Interfaces
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
            /*IList<FileInfo> logFiles = GetLogFiles(false).Select(f => new FileInfo(f)).ToList();
            FileInfo selectedLogFile = logFiles.FirstOrDefault(lf => string.Equals(lf.Name, fileName));
            if (selectedLogFile == null)
                return string.Empty;
            return File.ReadAllText(selectedLogFile.FullName);*/
            return null;
        }

        private IList<FileInfo> BuildLogFileList()
        {
            List<string> directories = new List<string>() {_logsRootDirectory};
            if (_includeSubDirs)
                directories.AddRange(Directory.GetDirectories(_logsRootDirectory));
            IList<FileInfo> logFilesInfo = new List<FileInfo>();
            foreach (string directory in directories)
            {
                //Directory.GetFiles()
            }
            return new List<FileInfo>();
        }

        private string _logsRootDirectory;
        private bool _includeSubDirs;
        private IList<string> _logFileFilters;
    }
}