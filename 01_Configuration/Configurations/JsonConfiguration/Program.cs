using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace JsonConfiguration
{
    class Program
    {
        public static IConfigurationRoot Configuration { get; set; }
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("awesomeConfig.json");

            Configuration = builder.Build();

            var pageSize = Configuration["Tables:PageSize"];

            var pageSizeFromSection = Configuration.GetSection("Tables:PageSize");


            foreach (var item in Configuration.AsEnumerable())
            {
                Console.WriteLine($"Key: {item.Key}, Value: {item.Value}");
            }
            Console.ReadKey();
        }
    }
}
