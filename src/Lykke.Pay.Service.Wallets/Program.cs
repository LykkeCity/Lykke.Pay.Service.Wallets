﻿using System;
using System.IO;
using System.Runtime.Loader;
using System.Threading;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Hosting;

namespace Lykke.Pay.Service.Wallets
{
    [UsedImplicitly]
    internal class Program
    {
        [UsedImplicitly]
        private static void Main(string[] args)
        {
            var webHostCancellationTokenSource = new CancellationTokenSource();
            var end = new ManualResetEvent(false);

            AssemblyLoadContext.Default.Unloading += ctx =>
            {
                Console.WriteLine("SIGTERM recieved");
                webHostCancellationTokenSource.Cancel(false);

                end.WaitOne();
            };

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseUrls("http://*:4566")
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();
            
            host.Run();

            end.Set();

            Console.WriteLine("Terminated");
        }
    }
}