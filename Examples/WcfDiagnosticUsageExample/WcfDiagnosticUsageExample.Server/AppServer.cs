using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Wcf.Diagnostics.Core.Data;
using WcfDiagnosticUsageExample.Common.Common;
using WcfDiagnosticUsageExample.Server.Events;

namespace WcfDiagnosticUsageExample.Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class AppServer : IAppServer
    {
        static AppServer()
        {
            Clients = new ConcurrentDictionary<string, IAppCallback>();
        }

        public bool Login(string userName, string password)
        {
            // todo: umv: this is example, we simply pass authentication
            OperationContext context = OperationContext.Current;           
            string key = context.SessionId ?? context.Channel.RemoteAddress.Uri.Host;
            if (!Clients.ContainsKey(key))
            {
                Clients[key] = context.GetCallbackChannel<IAppCallback>();
                ConnectedStateChangedHandler(new ClientConnectedEventArgs(true, key, Clients[key]));
            }
            return true;
        }

        public bool Logout()
        {
            OperationContext context = OperationContext.Current;
            string key = context.SessionId ?? context.Channel.RemoteAddress.Uri.Host;
            if (Clients.ContainsKey(key))
            {
                ConnectedStateChangedHandler(new ClientConnectedEventArgs(false, key));
                Clients.Remove(Clients.First(c => c.Key == key));
                return true;
            }
            return false;
        }

        public IList<LogInfo> GetLogFiles(string clientId)
        {
            IAppCallback client = Clients[clientId];
            if(client == null)
                throw new ArgumentException($"client with id {clientId} was not found");
            IList<LogInfo> logs = client.GetLogsFiles();
            return logs;
        }

        public string GetLogFile(string clientId, string fileName)
        {
            IAppCallback client = Clients[clientId];
            if(client == null)
                throw new ArgumentException($"client with id {clientId} was not found");
            string logContent = client.GetLogFile(fileName);
            return logContent;
        }

        public string GetVersion(string clientId)
        {
            IAppCallback client = Clients[clientId];
            if(client == null)
                throw new ArgumentException($"client with id {clientId} was not found");
            return client.GetVersion();
        }

        public IList<string> GetEnvironmentVariables(string clientId) 
        {
            IAppCallback client = Clients[clientId];
            if(client == null)
                throw new ArgumentException($"client with id {clientId} was not found");
            IList<string> environment = client.GetEnvironmentDescription();
            return environment;
        }

        private void ConnectedStateChangedHandler(ClientConnectedEventArgs args)
        {
            EventHandler<ClientConnectedEventArgs> handler = OnConnectedStateChanged;
            if (handler != null)
                handler(this, args);
        }

        public static event EventHandler<ClientConnectedEventArgs> OnConnectedStateChanged;

        public static IDictionary<string, IAppCallback> Clients { get; }

    }
}