using System;

namespace Floki.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException()
            : base()
        {
        }

        public ResourceNotFoundException(string message)
            : base(message)
        {
        }

        public ResourceNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ResourceNotFoundException(string name, object id)
            : base($"Resouce: {name} with identifier: ({id}) was not found.")
        {
        }
  }
}