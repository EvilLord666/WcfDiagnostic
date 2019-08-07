using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using Wcf.Diagnostics.Core.Data;
using WcfDiagnosticUsageExample.Common.Common;
using WcfDiagnosticUsageExample.Server;

namespace WcfDiagnosticUsageExample.ServerCli
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // Create server
            WSDualHttpBinding binding = new WSDualHttpBinding();
            binding.Name = "TestService";
            binding.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
            binding.Security.Mode = WSDualHttpSecurityMode.None;
            // binding.ClientBaseAddress = new Uri(ClientBaseUri);

            ServiceHost serviceHost = new ServiceHost(typeof(AppServer), new Uri("http://127.0.0.1:8000"));
            serviceHost.OpenTimeout = TimeSpan.FromSeconds(10);
            serviceHost.AddServiceEndpoint(typeof(IAppServer), binding, "appService");
            ServiceDebugBehavior debugBehavior = serviceHost.Description.Behaviors.FirstOrDefault(item => item.GetType() == typeof(ServiceDebugBehavior)) as ServiceDebugBehavior;
            if (debugBehavior != null)
                debugBehavior.IncludeExceptionDetailInFaults = true;
            serviceHost.Opened += (sender, eventArgs) =>
            {
                Console.WriteLine("Server host was opened");
            };
            serviceHost.Open(TimeSpan.FromSeconds(10));
            
            AppServer.OnConnectedStateChanged += (sender, eventArgs) =>
            {
                if (eventArgs.Connected)
                {
                    Console.WriteLine($"Client: {eventArgs.ClientId} connected.");
                    string version = eventArgs.ClientCallback.GetVersion();
                    Console.WriteLine($"Client Version is: {version}");
                    IList<LogInfo> logFiles = eventArgs.ClientCallback.GetLogsFiles();
                    if (logFiles != null)
                    {
                        Console.WriteLine("Client logs files are: ");
                        foreach (LogInfo logFile in logFiles)
                        {
                            Console.WriteLine($"\t\t:{logFile.FileName}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Client: {eventArgs.ClientId} disconnected.");
                }
            };
            
            Console.WriteLine("Press any input plus Enter to stop the server!");
            string input = Console.ReadLine();
            
            serviceHost.Close();
        }
    }
}