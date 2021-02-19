using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingBackend.Exceptions
{
    public class UserAlreadyRegisteredException : HttpExceptionBase
    {
        public UserAlreadyRegisteredException() : base("User already registered")
        {
        }
    }
}
