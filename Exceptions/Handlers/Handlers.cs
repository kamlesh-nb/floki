namespace Floki.Exceptions
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
  public class Handlers
  {
    public static void HandleForbidden(ExceptionContext context)
    {
      var details = new ProblemDetails
      {
          Status = StatusCodes.Status403Forbidden,
          Title = "Forbidden",
          Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3"
      };

      context.Result = new ObjectResult(details)
      {
          StatusCode = StatusCodes.Status403Forbidden
      };
    }

    public static void HandlerUnAuthorized(ExceptionContext context)
    {
      var details = new ProblemDetails
      {
        Status = StatusCodes.Status401Unauthorized,
        Title = "Unauthorized",
        Type = "https://tools.ietf.org/html/rfc7235#section-3.1"
      };

      context.Result = new ObjectResult(details)
      {
        StatusCode = StatusCodes.Status401Unauthorized
      };
    }

    public static void HandleResourceNotFound(ExceptionContext context)
    {
      var exception = context.Exception.InnerException as ResourceNotFoundException;
      var pd = new ProblemDetails()
      {
        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
        Title = "The specified resource was not found.",
        Detail = exception?.Message
      };
      context.Result = new NotFoundObjectResult(pd)
      {
        StatusCode = StatusCodes.Status404NotFound
      };
    }

    public static void HandlerValidationFailures(ExceptionContext context)
    {
      var exception = context.Exception.InnerException as ValidationException;

      var pd = new ValidationProblemDetails(exception?.Failures)
      {
        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
      };
      context.Result = new BadRequestObjectResult(pd)
      {
        StatusCode = StatusCodes.Status400BadRequest
      };
    }
  }
}