using System;
using System.Net;

namespace Prototype.Models
{
    public class ErrorMessage
    {
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
