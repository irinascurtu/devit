using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ApiErrors;
using Newtonsoft.Json;

namespace Common
{
    public static class HttpResponseMessageExtension
    {
        public static async Task<ErrorResponse> ExceptionResponse(this HttpResponseMessage httpResponseMessage)
        {
            string responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
            ErrorResponse exceptionResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);
            return exceptionResponse;
        }
    }
}
