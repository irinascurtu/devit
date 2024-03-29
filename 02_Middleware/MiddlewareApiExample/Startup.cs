﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Middleware.MiddlewareExtensions;
using Middleware.Middlewares;
using MiddlewareApiExample.Middlewares;

namespace MiddlewareApiExample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
            });

            services.Configure<RequestCultureOptions>(Configuration.GetSection("RequestCultureOptions"));

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
                // app.UseHsts();

            }

            app.UseStatusCodePages("text/plain", "HTTP Error - Status Code: {0}");
            app.UseStatusCodePagesWithRedirects("/error/{0}");
            app.UseStatusCodePagesWithReExecute("/error/{0}");

               app.UseCookiePolicy();
            // app.UseStaticFiles();
            // app.UseAuthentication();
            // app.UseSecurityHeadersMiddleware();

            app.UseRequestCultureMiddleware();

            //app.Use(CheckUserAgent);
            app.UseMvc();
        }



        private RequestDelegate CheckUserAgent(RequestDelegate next)
        {
            return async ctx =>
            {
                await ctx.Response.WriteAsync(CheckUserAgent(ctx));
                //    await next();
            };
        }

        public static string CheckUserAgent(HttpContext context)
        {
            var ua = context.Request.Headers["user-agent"].ToString();

            return ua;
        }

    }
}
