using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Wcf.Diagnostics.Core.Data;
using Wcf.Diagnostics.Core.Interfaces;
using Wcf.Diagnostics.NetCore.Impl.InterfacesImpl;
using WcfDiagnosticUsageExample.Common.Common;

namespace WcfDiagnosticUsageExample.Client
{
    public class AppClient : SimpleLogsCaptureCallback, IAppCallback
    {
        public AppClient(string logsRootDirectory, bool includeSubDirs, IList<string> logFileFilters, string appServiceEndpoint) 
            : base(logsRootDirectory, includeSubDirs, logFileFilters)
        {
            WSDualHttpBinding binding = new WSDualHttpBinding(WSDualHttpSecurityMode.None);
            EndpointAddress endpointAddress = new EndpointAddress(appServiceEndpoint);
            InstanceContext context = new InstanceContext(this);
            DuplexChannelFactory<IAppServer> channelFactory = new DuplexChannelFactory<IAppServer>(context, binding, endpointAddress);

            _server = channelFactory.CreateChannel();
        }

        public bool Start()
        {
            bool result = _server.Login("admin", "admin");
            return result;
        }

        public bool Stop()
        {
            bool result = _server.Logout();
            return result;
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
        
        private readonly IAppServer _server;
    }
}