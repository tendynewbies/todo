using System;
using System.Net;

namespace ToDo.Model
{
    public class ServiceResponse<T>
    {
        public Exception Exception { get; set; }
        public bool IsSuccess { get; set; }
        public HttpStatusCode ResponseCode { get; set; }
        public T Result { get; set; }
    }
}
