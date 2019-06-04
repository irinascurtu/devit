using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HealthChecks
{
    public class BeatPulseStartup
    {
        #region snippet_ConfigureServices
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBeatPulse();
            //use to check endpoints for reposnses
            services.AddHealthChecks()
            .AddUrlGroup(new Uri("https://api.simplecast.com/v1/podcasts.json"), "Simplecast API", HealthStatus.Degraded)
            .AddUrlGroup(new Uri("https://rss.simplecast.com/podcasts/4669/rss"), "Simplecast RSS", HealthStatus.Degraded);

        }
        #endregion

        #region snippet_Configure
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseHealthChecks("/health", new HealthCheckOptions()
            {
                AllowCachingResponses = false,
                ResponseWriter = WriteResponse
            });

            app.UseBeatPulse();

            #endregion

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(
                    "Navigate to /health to see the health status.");
            });
        }

        #region response
        private static Task WriteResponse(HttpContext httpContext,
            HealthReport result)
        {
            httpContext.Response.ContentType = "application/json";

            var json = new JObject(
                new JProperty("status", result.Status.ToString()),
                new JProperty("results", new JObject(result.Entries.Select(pair =>
                    new JProperty(pair.Key, new JObject(
                        new JProperty("status", pair.Value.Status.ToString()),
                        new JProperty("description", pair.Value.Description),
                        new JProperty("data", new JObject(pair.Value.Data.Select(
                            p => new JProperty(p.Key, p.Value))))))))));
            return httpContext.Response.WriteAsync(
                json.ToString(Formatting.Indented));
        }
        #endregion
    }
}
