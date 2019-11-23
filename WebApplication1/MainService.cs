using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class MainService
    {
        private string[] args;
        public MainService(string[] vs)
        {
            args = vs;
        }
        public void Start()
        {
            var isService = !(Debugger.IsAttached || args.Contains("--console"));
            var builder = CreateWebHostBuilder(args.Where(arg => arg != "--console").ToArray());

            if (isService)
            {
                var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
                var pathToContentRoot = Path.GetDirectoryName(pathToExe);
                builder.UseContentRoot(pathToContentRoot);
            }

            var host = builder.Build();
            host.RunAsync();
        }

        public void Stop()
        {
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var config = new ConfigurationBuilder()
    // .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("config.json", optional: true, reloadOnChange: true)
    .Build();

            return WebHost.CreateDefaultBuilder(args)
                    .UseKestrel()
                    .UseUrls("http://localhost:6004")
                    .UseConfiguration(config)
                    .UseStartup<Startup>();
        }
    }
}
