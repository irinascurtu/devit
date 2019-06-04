using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace HealthChecks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
              .UseStartup<Startup>();

        // .UseStartup<Startup>();

        public static IWebHostBuilder CreateWebHostBuilderForBeatPulse(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseBeatPulse(options =>
                {
                    options.ConfigurePath(path: "health") //default hc
                        .ConfigureTimeout(milliseconds: 1500) // default -1 infinitely
                        .ConfigureDetailedOutput(detailedOutput: true,
                            includeExceptionMessages: true); //default (true,false)
                }).UseStartup<BeatPulseStartup>();
    }
}
