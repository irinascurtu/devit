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
    public class StartupWithMapWhen
    {

        public StartupWithMapWhen(IConfiguration configuration)
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

            #region Map
            //app.Map(new PathString("/map"),
            //    a => a.Use(async (context, next) =>
            //    {
            //        await context.Response.WriteAsync("Map!\n");
            //        await next();
            //    }));

            //app.Run(async context =>
            //    {
            //        await context.Response.WriteAsync("Hello world");
            //    });
            #endregion

            //#region MapWhen
            ////https://localhost:53285/api/values?branch=coolbranch
            app.MapWhen(context => context.Request.Query.ContainsKey("branch"), HandleBranchWithMapWhen);
            // app.MapWhen(context => context.Request.Query.ContainsKey("branch") && context.Request.Query["branch"] == "coolbranch", HandleBranchWithMapWhen);

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("bye...but you won't even see me if you MapWhen");
                await next();
            });

            //#endregion

            app.UseMvc();
        }

        private static void HandleBranchWithMapWhen(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
             {
                 var branchVer = context.Request.Query["branch"];
                 await context.Response.WriteAsync($"Mapped with MapWhen for the {branchVer} branch");
                 await next();
             });

        }
    }
}
