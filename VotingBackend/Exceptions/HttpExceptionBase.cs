using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingBackend.Exceptions
{
    public class HttpExceptionBase : Exception
    {
        public string Message { get; set; }

        public HttpExceptionBase(string message)
        {
            Message = message;
        }
    }
}
