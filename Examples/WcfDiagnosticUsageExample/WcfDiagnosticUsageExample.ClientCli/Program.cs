using System;
using System.Collections.Generic;
using WcfDiagnosticUsageExample.Client;

namespace WcfDiagnosticUsageExample.ClientCli
{
    class Program
    {
        static void Main(string[] args)
        {
            AppClient client = new AppClient(@"..\..\..\logs", true, new List<string>() {"*.log"}, 
                                              "http://127.0.0.1:8000/appService/" );
            
            bool result = client.Start();
            
            Console.WriteLine($"Client connection status: {result}");
            Console.WriteLine($"To stop client make any input and press enter");
            string input = Console.ReadLine();
            client.Stop();
        }
    }
}