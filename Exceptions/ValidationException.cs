using System;
using System.Collections.Generic;

namespace Floki.Exceptions
{
  public class ValidationException : Exception
  {
    public ValidationException(string message) : base(message) { }
    public ValidationException(string message, Exception innerException) : base(message, innerException) { }
    public ValidationException(IDictionary<string, string[]> errors)
    {
      Failures = errors;
    }
    public IDictionary<string, string[]> Failures { get; set; }
  }
}