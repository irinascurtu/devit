using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApiErrors.Error
{
    public static class HttpContextExtensions
    {
        public static async Task WriteErrorResponse(this HttpContext context, HttpStatusCode statusCode, ErrorResponse errorResponse)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = new MediaTypeHeaderValue("application/json").ToString();
            await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse), Encoding.UTF8);
        }
    }
}
