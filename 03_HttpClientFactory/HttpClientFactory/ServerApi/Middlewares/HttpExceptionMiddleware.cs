using System;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ApiErrors.Error;
using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ApiErrors.Middlewares
{
    public class HttpExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<HttpExceptionMiddleware> logger;

        public HttpExceptionMiddleware(RequestDelegate next, ILogger<HttpExceptionMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next.Invoke(context);
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, ex.Message);

                var content = new ErrorResponse(HttpStatusCode.InternalServerError, ex.Message)
                {
                    RequestUrl = context.Request.Path
                };

                await context.WriteErrorResponse(HttpStatusCode.InternalServerError, content);
            }
            catch (ApiException httpException)
            {
                var content = new ErrorResponse(httpException.StatusCode, httpException.Message)
                {
                    RequestUrl = context.Request.Path
                };

                await context.WriteErrorResponse(httpException.StatusCode, content);
            }
        }
    }
}
