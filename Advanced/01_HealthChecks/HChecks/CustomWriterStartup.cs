using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HealthChecks
{
    public class CustomWriterStartup
    {
        #region snippet_ConfigureServices
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();
        }
        #endregion

        #region snippet_Configure
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseHealthChecks("/health", new HealthCheckOptions()
            {
                AllowCachingResponses = false,
                ResultStatusCodes =
                {
                    [HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [HealthStatus.Degraded] = StatusCodes.Status200OK,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                },
                ResponseWriter = WriteResponse
            });

            #endregion

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(
                    "Navigate to /health to see the health status.");
            });
        }

        #region snippet_WriteResponse
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
