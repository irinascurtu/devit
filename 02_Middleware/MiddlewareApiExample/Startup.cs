using System;
using System.Collections.Generic;
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
            
            // app.Use(GreetingMiddleware);
            //app.Use(SayByeToPeople);
            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("<div>Inside middleware defined using app.Run</div>");
            //});

            //  app.Use(FirstStep);
            //app.UseMiddleware<SecurityHeadersMiddleware>();
            //app.Use(SayByeToPeople);

            //app.Use(SecondStep);
            //  app.Map(new PathString("/foo"), HandleBranchWithMapWhen);

            //app.MapWhen(
            //    context => context.Request.Path.StartsWithSegments(new PathString("/foo")),

            //    a => a.Use(async (context, next) =>
            //    {
            //        Console.WriteLine("B (before)");
            //        await next();
            //        Console.WriteLine("B (after)");
            //    }));

            //app.Map(new PathString("/map"),
            //  a => a.Use(async (context, next) =>
            //  {
            //      await context.Response.WriteAsync("Map!\n");

            //  }));

            ////https://localhost:44307/when
            //app.MapWhen(context => context.Request.Path.StartsWithSegments(new PathString("/when")),
            //    HandleBranchWithMapWhen);

            ////https://localhost:44307/api/values?branch=coolbranch
            //app.MapWhen(context => context.Request.Query.ContainsKey("branch"), HandleBranchWithMapWhen);

            //app.UseWhen(
            //        context => context.Request.Path.StartsWithSegments(new PathString("/foo")),
            //        a => a.Use(async (context, next) =>
            //        {                       
            //            await context.Response.WriteAsync("foo branch!before");
            //            await next();
            //            await context.Response.WriteAsync("foo branch! after");
            //        }));


            app.Use(async (context, next) =>
            {

                await context.Response.WriteAsync("A (before)");              
                await next();       

                await context.Response.WriteAsync("A (after)");
            });

            app.UseWhen(
                context => context.Request.Path.StartsWithSegments(new PathString("/foo")),
                a => a.Use(async (context, next) =>
                {
                    Console.WriteLine("B (before)");
                    await next();
                    Console.WriteLine("B (after)");
                }));

            app.Run(async context =>
            {
                Console.WriteLine("C");
                await context.Response.WriteAsync("Hello world");
            });

            //app.UseMiddleware<LimiterMiddleware>();

            //app.Use(SecondStep);
            //order matters
            // app.UseCookiePolicy();
            // app.UseStaticFiles();
            // app.UseAuthentication();
            // app.UseSecurityHeadersMiddleware();


            app.UseMvc();
        }


        //step 1
        private RequestDelegate GreetingMiddleware(RequestDelegate next)
        {
            return async ctx =>
            {
                await ctx.Response.WriteAsync("get only THE hello!");

                //if (ctx.Request.Path.StartsWithSegments("/hello"))
                //{
                //    await ctx.Response.WriteAsync("hello you!");
                //}
                //else
                //{
                //    await next(ctx);
                //}
            };
        }

        private RequestDelegate CheckIE(RequestDelegate next)
        {
            return async ctx =>
            {
                //    var isfromInternetExplorer = context.IsFromInternetExplored();
                //    if (isfromInternetExplorer)
                //    {
                //        await context.Response.WriteAsync("IE Detected!!");
                //        return;
                //    }

                //    await next();
            };
        }


        ///add mapwhen, 
        ///
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

        private RequestDelegate SayByeToPeople(RequestDelegate next)
        {
            return async ctx =>
            {
                await ctx.Response.WriteAsync("buh bye!");

            };
        }

        /// <summary>
        /// localhost:1234/?branch=master
        /// </summary>
        /// <param name="app"></param>

        private static void HandleBranchWithMapWhen(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var branchVer = context.Request.Query["branch"];
                await context.Response.WriteAsync($"Branch used = {branchVer}");
            });
        }


    }
}
