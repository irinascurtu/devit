using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Complete
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
              //.UseStartup(typeof(Startup).GetTypeInfo().Assembly.FullName); //for convention based will start looking for ConfigureDevelopment, ConfigureProduction
              //or you should add different startup classes : StartupProduction -> based on the environment variables
                            .UseStartup<Startup>();
    }
}
