using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Serialization;
using RiskFirst.Hateoas;
using SuperHeroes.Middleware;
using SuperHeroes.OutputFormatters;

namespace SuperHeroes
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
            _ = services.AddMvc(options =>
              {   //406 if no formatter is found
                  options.ReturnHttpNotAcceptable = true;
                  //honors browser accept headers
                  options.RespectBrowserAcceptHeader = true;

                  options.FormatterMappings.SetMediaTypeMappingForFormat("xml", "application/xml");

                  options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
                  options.OutputFormatters.Add(new AwesomeOutputFormatter());


                  //or add your custom formatter
                  //options.OutputFormatters.RemoveType<TextOutputFormatter>();

                  //null return is mapped to no content if not removed
                  //when removed, body is null with 200 status
                  // options.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();

              })
            .AddJsonOptions(jsonOptions =>
             {
                 jsonOptions.SerializerSettings.ContractResolver = new DefaultContractResolver()
                 {
                     NamingStrategy = new SnakeCaseNamingStrategy()

                 };

             });



            //services.AddLinks(config =>
            //{
            //    config.AddPolicy<SuperHero>(policy => {
            //        policy.RequireSelfLink()
            //            .RequireRoutedLink("all", "GetAllModelsRoute")
            //            .RequireRoutedLink("delete", "DeleteModelRoute", x => new { id = x.Id });
            //    });
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // app.UseETagger();
            //  app.UseHeadersMiddleware();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
