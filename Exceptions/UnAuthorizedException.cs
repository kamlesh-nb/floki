using System;

namespace Floki.Exceptions
{
    public class UnAuthorizedException : Exception  
    {
        public UnAuthorizedException() { }
        public UnAuthorizedException(string message) : base(message) { }
    }
}