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

    public class StartUpWithDelegates
    {
        public StartUpWithDelegates(IConfiguration configuration)
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

            app.Use(FirstStep);
            // app.Use(SecondStep);
            //has no next :)
            app.Run(async context =>
            {
                Console.WriteLine("C");
                await context.Response.WriteAsync("Hello world");
            });
            app.Use(SecondStep);
            app.UseMvc();
        }

        private RequestDelegate FirstStep(RequestDelegate next)
        {
            return async ctx =>
            {
                await ctx.Response.WriteAsync("FirstStep!\n");
                await next(ctx);
            };
        }

        private RequestDelegate SecondStep(RequestDelegate next)
        {
            return async ctx =>
            {
                await ctx.Response.WriteAsync("SecondStep!\n");
                await next(ctx);
            };
        }

    }
}
