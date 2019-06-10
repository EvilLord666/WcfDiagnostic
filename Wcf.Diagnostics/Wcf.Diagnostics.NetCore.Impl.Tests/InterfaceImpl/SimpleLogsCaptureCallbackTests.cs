using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using Wcf.Diagnostics.NetCore.Impl.Tests.TestUtils;
using Xunit;

namespace Wcf.Diagnostics.NetCore.Impl.Tests.InterfaceImpl
{
    public class SimpleLogsCaptureCallbackTests
    {
        [Fact]
        public void TestLogsCapture()
        {
            // Create server
            BasicHttpBinding binding = new BasicHttpBinding();
            binding.Name = "TestService";
            binding.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
            binding.Security.Mode = BasicHttpSecurityMode.None;
            ServiceHost serviceHost = new ServiceHost(typeof(TestWcfService), new Uri(BaseUri));
            serviceHost.AddServiceEndpoint(typeof(ITestWcfService), binding, new Uri(EndpointUri));
            serviceHost.Open();
            //  Create channel for client
            Binding serviceBinding = new BasicHttpBinding(BasicHttpSecurityMode.None);
            EndpointAddress endpointAddress = new EndpointAddress(EndpointUri);
            ChannelFactory<ITestWcfService> channelFactory = new ChannelFactory<ITestWcfService>(serviceBinding, endpointAddress);
            ITestWcfService server4Client = channelFactory.CreateChannel();

            int sessionId = server4Client.LogIn("MyDomain", "admin", "123");

            // todo: umv: add here call to get logs and check

            server4Client.LogOut(sessionId);
            serviceHost.Close();
        }

        private const string BaseUri = "http://0.0.0.0:8123/testwcfservice";
        private const string EndpointUri = "http://0.0.0.0:8123/testwcfservice/test";
    }
}
