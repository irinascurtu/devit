using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Middleware.Middlewares
{
    internal class SecurityHeadersMiddleware
    {
        private readonly RequestDelegate next;

        public SecurityHeadersMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.Headers.Add("X-Xss-Protection", "1; mode=block");
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            context.Response.Headers.Add("X-Frame-Options", "DENY");
            context.Response.Headers.Add("X-Permitted-Cross-Domain-Policies", "none");

            await this.next.Invoke(context);
        }
    }
}
