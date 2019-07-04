using System;
using System.Net;
using System.Runtime.Serialization;

namespace Common
{
    [Serializable]
    public class ApiException : Exception
    {
        public ApiException()
        {
        }

        public ApiException(string message) : base(message)
        {
            this.StatusCode = HttpStatusCode.InternalServerError;
        }

        public ApiException(string message, string v) : base(message)
        {
        }

        public ApiException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ApiException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public HttpStatusCode StatusCode { get; internal set; }
        
       
        public override string ToString()
        {
            return "ApiException";
        }
    }
}