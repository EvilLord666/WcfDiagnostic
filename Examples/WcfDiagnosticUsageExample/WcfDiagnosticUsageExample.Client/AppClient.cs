using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Wcf.Diagnostics.NetCore.Impl.InterfacesImpl;
using WcfDiagnosticUsageExample.Common.Common;

namespace WcfDiagnosticUsageExample.Client
{
    public class AppClient : SimpleLogsCaptureCallback, IAppCallback
    {
        public AppClient(string logsRootDirectory, bool includeSubDirs, IList<string> logFileFilters) 
            : base(logsRootDirectory, includeSubDirs, logFileFilters)
        {
            NetHttpBinding binding = new NetHttpBinding(BasicHttpSecurityMode.None);
            EndpointAddress endpointAddress = new EndpointAddress(AppServiceEndpointUri);
            InstanceContext context = new InstanceContext(this);
            DuplexChannelFactory<IAppServer> channelFactory = new DuplexChannelFactory<IAppServer>(context, binding, endpointAddress);

            _server = channelFactory.CreateChannel();
        }

        public void Start()
        {
            bool result = _server.Login("admin", "admin");
        }

        public void Stop()
        {
            bool result = _server.Logout();
        }

        public string GetVersion()
        {
            return ClientVersion;
        }

        // following code is just for example
        public IList<string> GetEnvironmentDescription()
        {
            return new List<string>()
            {
                "ComputerName: TestMachine01",
                "Configuration: Test(beta)",
                "Mode: RO"
            };
        }

        private const string ClientVersion = "1.0_beta";
        private const string AppServiceEndpointUri = "http://127.0.0.1:8000/appService/";
        
        private readonly IAppServer _server;
    }
}