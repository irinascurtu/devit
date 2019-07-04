using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ApiErrors.Error
{
    [DebuggerDisplay("Field={Field}, {Message}")]
    public class ValidationError
    {
        public ValidationError(string field, string message)
        {
            this.Field = field;
            this.Message = message;
        }

        /// <summary>
        /// Field that failed validation
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Why field failed validation.
        /// Typically error code
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Message provided to service consumer
        /// </summary>
        public string Message { get; set; }
    }
}
