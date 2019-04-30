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

        public LogInfo(string fileName)
        {
            FileName = fileName;
        }

        [DataMember]
        public string FileName { get; set; }
    }
}
