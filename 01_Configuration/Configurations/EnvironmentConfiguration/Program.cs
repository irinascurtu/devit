using Microsoft.Extensions.Configuration;
using System;

namespace EnvironmentConfiguration
{
    class Program
    {

        public IConfiguration Configuration { get; }

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
             //  .AddEnvironmentVariables("RANDOM_");
            // .AddEnvironmenVariables();
           // Configuration = builder.Build();

          //  Console.WriteLine(Configuration["RANDOM_VALUE"]);
        }
    }
}
