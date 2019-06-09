using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Middleware.MiddlewareExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlewareApiExample
{
    public class StartupWithGreetings
    {

        public StartupWithGreetings(IConfiguration configuration)
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
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // app.Use(GreetingMiddleware);
           

            app.UseGreetingsMiddleware();
            app.Use(SayByeToPeople);
            app.UseMvc();
        }

        //step 1
        private RequestDelegate GreetingMiddleware(RequestDelegate next)
        {
            return async ctx =>
            {
                await ctx.Response.WriteAsync("get only THE hello!");

            };
        }

        private RequestDelegate SayByeToPeople(RequestDelegate next)
        {
            return async ctx =>
            {
                await ctx.Response.WriteAsync("\n buh bye!");
            };
        }
    }
}
