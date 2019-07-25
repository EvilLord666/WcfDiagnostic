using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Threading;
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
            //binding.
            
            ServiceHost serviceHost = new ServiceHost(typeof(TestWcfService), new Uri(BaseUri));
            serviceHost.OpenTimeout = TimeSpan.FromSeconds(10);
            serviceHost.AddServiceEndpoint(typeof(ITestWcfService), binding, "testwcfservice");
            serviceHost.AddServiceEndpoint(typeof(ITestWcfService), new WSHttpBinding(), "mex");

            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            smb.HttpGetUrl = new Uri("http://0.0.0.0:8123/mex");
            serviceHost.Description.Behaviors.Add(smb);

            serviceHost.Open(TimeSpan.FromSeconds(10));
            serviceHost.Opened += (sender, args) =>
            {
                System.Console.WriteLine("Service Host Was Opened!");
            };
            //Thread.Sleep(TimeSpan.FromSeconds(10));
            //  Create channel for client
            Binding serviceBinding = new BasicHttpBinding(BasicHttpSecurityMode.None);
            EndpointAddress endpointAddress = new EndpointAddress(EndpointUri);
                //EndpointUri);
            ChannelFactory<ITestWcfService> channelFactory = new ChannelFactory<ITestWcfService>(serviceBinding, endpointAddress);
            ITestWcfService server4Client = channelFactory.CreateChannel();

            int sessionId = server4Client.LogIn("MyDomain", "admin", "123");

            // todo: umv: add here call to get logs and check

            server4Client.LogOut(sessionId);
            serviceHost.Close();
        }

        private const string BaseUri = "http://0.0.0.0:8123/";
        private const string EndpointUri = "http://0.0.0.0:8123/testwcfservice/";
    }
}
