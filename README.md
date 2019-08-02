# Wcf Diagnostics
An simple wcf service callback interface and it's implementation for accessing WCF client logs **FROM SERVER** (very useful when you have to understand what is happended on client side but you have security restriction to access client PC's via RDP, TeamViewer or other Tools.
Interface is a very simple only 2 methods - GetLogsFiles (list of logs files) and GetLogFile by FileName. Independent from log sybsystem (could be used any: Serilog, Nlog, log4net, e.t.c.)

# How to use
Full example is demonstrated in Unit Test - SimpleLogsCaptureCallbackTests in which i manually start test WCF service with callback contract - ILogsCaptureCallback, starting client and getting Logs files list anf log file for specified client. Some specific (Stored clients was implemented in TestWcfService, see it):

// Create server
            WSDualHttpBinding binding = new WSDualHttpBinding();
            binding.Name = "TestService";
            binding.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
            binding.Security.Mode = WSDualHttpSecurityMode.None;
            binding.ClientBaseAddress = new Uri(ClientBaseUri);

            ServiceHost serviceHost = new ServiceHost(typeof(TestWcfService), new Uri(ServerBaseUri));
            serviceHost.OpenTimeout = TimeSpan.FromSeconds(10);
            serviceHost.AddServiceEndpoint(typeof(ITestWcfService), binding, "testService");
            ServiceDebugBehavior debugBehavior = serviceHost.Description.Behaviors.FirstOrDefault(item => item.GetType() == typeof(ServiceDebugBehavior)) as ServiceDebugBehavior;
            if (debugBehavior != null)
                debugBehavior.IncludeExceptionDetailInFaults = true;
            //serviceHost.Description.Behaviors.Add(new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });
            // ITestWcfService server = new TestWcfService();
            
            serviceHost.Opened += (sender, args) =>
            {
                System.Console.WriteLine("Service Host Was Opened!");
            };
            
            serviceHost.Open(TimeSpan.FromSeconds(10));
            
            WSDualHttpBinding serviceBinding = new WSDualHttpBinding(WSDualHttpSecurityMode.None);
            
            EndpointAddress endpointAddress = new EndpointAddress(TestServiceEndpointUri);
            // client channel creation
            DuplexChannelFactory<ITestWcfService> channelFactory = new DuplexChannelFactory<ITestWcfService>(new SimpleLogsCaptureCallback(@"..\..\..\TestLogs", true, new[] {"*.log"}),
                                                                                                             serviceBinding, endpointAddress);
            
            ITestWcfService client = channelFactory.CreateChannel();
            
            ITestWcfService server = new TestWcfService();

            int sessionId = client.LogIn("MyDomain", "admin", "123");

            Assert.True(sessionId < 1000);

            IDictionary<string, ILogsCaptureCallback> clients = TestWcfService.Clients;
            Assert.Equal(1, clients.Count);
            IList<LogInfo> clientLogsFiles = server.GetClientLogs(clients.First().Key);
            Assert.Equal(3, clientLogsFiles.Count);
            string logFileContent = server.GetLogFile(clients.First().Key, "actual.log");
            Assert.Equal("Current log file (actual).", logFileContent);
            bool result = client.LogOut(sessionId);
            Assert.True(result);
            serviceHost.Close();

# Nuget Package
https://www.nuget.org/packages/WcfDiagnostic/
