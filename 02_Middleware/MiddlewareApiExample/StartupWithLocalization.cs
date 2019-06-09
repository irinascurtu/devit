using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlewareApiExample
{
    public class StartupWithLocalization
    {

        public StartupWithLocalization(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Localization
            //var languages = Configuration.GetSection("languages").Get<string[]>();
            //var supportedCultures = GlobalAppSettings.GetSupportedCultures(languages);
            //var defaultCulture = supportedCultures.Any() ? supportedCultures[0] : new CultureInfo("en-US");
            //app.UseRequestLocalization(new RequestLocalizationOptions
            //{
            //    DefaultRequestCulture = new RequestCulture(defaultCulture),
            //    SupportedCultures = supportedCultures,      // Formatting numbers, dates, etc.
            //    SupportedUICultures = supportedCultures     // UI strings that we have localized.
            //});
            app.UseMvc();
        }

       
    }
}
