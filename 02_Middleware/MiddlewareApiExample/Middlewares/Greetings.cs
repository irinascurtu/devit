using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlewareApiExample.Middlewares
{
    public class Greetings
    {
        private readonly RequestDelegate next;

        public Greetings(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await context.Response.WriteAsync("get only THE hello!");

            await this.next.Invoke(context);
        }
    }
}
