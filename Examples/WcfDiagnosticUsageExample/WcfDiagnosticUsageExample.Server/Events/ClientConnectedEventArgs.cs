using System;
using WcfDiagnosticUsageExample.Common.Common;

namespace WcfDiagnosticUsageExample.Server.Events
{
    public class ClientConnectedEventArgs : EventArgs
    {
        public ClientConnectedEventArgs(bool connected, string clientId, IAppCallback clientCallback = null)
        {
            Connected = connected;
            ClientId = clientId;
            ClientCallback = clientCallback;
        }

        public string ClientId { get; set; }
        public IAppCallback ClientCallback { get; set; }
        public bool Connected { get; set; }
    }
}