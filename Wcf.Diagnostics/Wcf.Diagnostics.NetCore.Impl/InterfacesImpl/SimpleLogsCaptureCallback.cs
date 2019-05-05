using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Wcf.Diagnostics.Core.Data;
using Wcf.Diagnostics.Core.Interfaces;

namespace Wcf.Diagnostics.NetCore.Impl.InterfacesImpl
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
            IList<FileInfo> logsFiles = BuildLogFileList();
            IList<LogInfo> logInfoResult = logsFiles.Select(lf => new LogInfo(lf.Name, lf.FullName)).ToList();
            return logInfoResult;
        }

        public string GetLogFile(string fileName)
        {
            FileInfo logFile = GetLogFileByName(fileName);
            if (logFile == null)
                return null;
            string logFileText = File.ReadAllText(logFile.FullName);
            return logFileText;
        }

        public async Task<string> GetLogFileAsync(string fileName)
        {
            FileInfo logFile = GetLogFileByName(fileName);
            if (logFile == null)
                return null;
            string logFileText = await File.ReadAllTextAsync(logFile.FullName);
            return logFileText;
        }

        private FileInfo GetLogFileByName(string fileName)
        {
            IList<FileInfo> logsFiles = BuildLogFileList();
            FileInfo logFile = logsFiles.FirstOrDefault(lf => string.Equals(lf.Name, fileName));
            return logFile;
        }

        private IList<FileInfo> BuildLogFileList()
        {
            List<string> directories = new List<string>() {_logsRootDirectory};
            if (_includeSubDirs)
                directories.AddRange(Directory.GetDirectories(_logsRootDirectory));
            List<FileInfo> logFilesInfo = new List<FileInfo>();
            foreach (string directory in directories)
            {
                foreach (string filter in _logFileFilters)
                {
                    string[] filteredFiles = Directory.GetFiles(directory, filter);
                    logFilesInfo.AddRange(filteredFiles.Where(f => logFilesInfo.Any(lf => !string.Equals(lf.FullName, f)))
                                                       .Select(f => new FileInfo(f)).ToList());
                }
                
            }
            return logFilesInfo;
        }

        private readonly string _logsRootDirectory;
        private readonly bool _includeSubDirs;
        private readonly IList<string> _logFileFilters;
    }
}