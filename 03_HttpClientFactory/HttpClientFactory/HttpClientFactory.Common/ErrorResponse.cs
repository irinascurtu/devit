using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using ApiErrors.Error;


namespace ApiErrors
{
    public class ErrorResponse
    {
        public ErrorResponse(HttpStatusCode code, string message)
          : this("api_error", code, message)
        {
        }

        public ErrorResponse(HttpStatusCode badRequest, IEnumerable<ValidationError> errors)
        {
            this.ErrorCode = badRequest;
            this.Errors = errors.ToList();
        }

        public ErrorResponse(string type, HttpStatusCode code, string message)
        {
            this.Type = type;
            this.ErrorCode = code;
            this.Message = message;

            this.Errors = new List<ValidationError>();
        }


        /// <summary>
        /// Request Url
        /// </summary>
        public string RequestUrl { get; set; }

        /// <summary>
        /// Text description of error code
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Http status code
        /// </summary>
        [DataMember(Name = "code")]
        public HttpStatusCode ErrorCode { get; set; }

        /// <summary>
        /// Http status code
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Nested errors
        /// </summary>
        public List<ValidationError> Errors { get; private set; }
    }
}