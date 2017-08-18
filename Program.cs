using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace todo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                 .AddCommandLine(args)
                 .AddEnvironmentVariables(prefix: "ASPNETCORE_")
                 .Build();

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
