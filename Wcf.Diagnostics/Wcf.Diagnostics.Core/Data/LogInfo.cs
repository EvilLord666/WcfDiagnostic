using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Wcf.Diagnostics.Core.Data
{
    [DataContract]
    public class LogInfo
    {
        public LogInfo()
        {
        }

        public LogInfo(string fileName, string fullPath)
        {
            FileName = fileName;
            FullPath = fullPath;
        }

        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public string FullPath { get; set; }
    }
}
