using Microsoft.Extensions.Configuration;
using System;

namespace EnvironmentConfiguration
{
    class Program
    {
        public static IConfigurationRoot Configuration { get; set; }

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()              
             .AddEnvironmentVariables();
             //.AddEnvironmentVariables("RANDOM_");
            Configuration = builder.Build();

            Console.WriteLine(Configuration["RANDOM_OTHER"]);
        }
    }
}
