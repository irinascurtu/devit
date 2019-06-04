using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HealthChecks
{
   //
    // This approach is suitable for systems that check for 'liveness' of an app with a database.

    #region snippet1
    public class DbHealthStartup
    {
        public DbHealthStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        #region snippet_ConfigureServices
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddSqlServer(Configuration["ConnectionStrings:DefaultConnection"]);
        }
        #endregion

        #region snippet_Configure
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseHealthChecks("/health");
            #endregion

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(
                    "Navigate to /health to see the health status.");
            });
        }
    }
    #endregion
}
