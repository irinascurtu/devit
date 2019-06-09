using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlewareApiExample
{
    public class StartupWithUseWhen
    {

        public StartupWithUseWhen(IConfiguration configuration)
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

            ////https://localhost:53285/api/values?usewhen=coolbranch
            app.UseWhen(context => context.Request.Query.ContainsKey("usewhen"), HandleBranchWithUseWhen);

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("\n bye...but you'll see me everytime in the pipeline");
                await next();
            });

            app.UseMvc();
        }

        private static void HandleBranchWithUseWhen(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Mapped when UseWhen and i'll pay it forward");
                await next();
            });

        }
    }
}
