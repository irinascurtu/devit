using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace SuperHeroes.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseHeadersMiddleware(this IApplicationBuilder application)
        {
            return application.UseMiddleware<HeadersMiddleware>();
        }
    }
}
