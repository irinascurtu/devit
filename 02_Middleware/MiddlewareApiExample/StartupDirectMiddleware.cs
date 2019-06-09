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
    public class StartupDirectMiddleware
    {
        public StartupDirectMiddleware(IConfiguration configuration)
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

            //1
            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("<div>Inside middleware defined using app.Run</div>");
            //});


            //1. order matters
            app.Use(async (context, next) =>
            {

                await context.Response.WriteAsync("A (before)");             

                await context.Response.WriteAsync("A (after)");

               await next();
            });


            app.Run(async context =>
            {
                Console.WriteLine("C");
                await context.Response.WriteAsync("Hello world");
            });
            app.UseMvc();
        }
    }
}
