using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SBSCoreTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //using (var host = WebHost.Start("http://192.168.1.222:8080", context => context.Response.WriteAsync("Hello WebHost")))
            //{
            //    Console.WriteLine("Application has been started");
            //    host.WaitForShutdown();
            //}
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>();
    }
}
