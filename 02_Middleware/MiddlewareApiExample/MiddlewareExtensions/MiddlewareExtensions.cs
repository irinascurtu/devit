using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Middleware.Middlewares;
using MiddlewareApiExample.Middlewares;

namespace Middleware.MiddlewareExtensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseSecurityHeadersMiddleware(this IApplicationBuilder application)
        {
            return application.UseMiddleware<SecurityHeadersMiddleware>();
        }
    }


    public static class Extensions
    {
        public static IApplicationBuilder UseGreetingsMiddleware(this IApplicationBuilder application)
        {
            return application.UseMiddleware<Greetings>();
        }

        public static IApplicationBuilder UseRequestCultureMiddleware(this IApplicationBuilder application)
        {
            return application.UseMiddleware<RequestCultureMiddleware>();
        }
    }
}
