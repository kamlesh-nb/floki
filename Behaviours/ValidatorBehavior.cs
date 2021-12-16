namespace Floki.Behaviours
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading;
  using System.Threading.Tasks;
  using Floki.Extensions;
  using FluentValidation;
  using MediatR;
  using Microsoft.Extensions.Logging;
  using ValidationException = Exceptions.ValidationException;

  public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
  {
    private readonly ILogger<ValidatorBehavior<TRequest, TResponse>> _logger;

    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidatorBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidatorBehavior<TRequest, TResponse>> logger)
    {
      _validators = validators;
      _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
      var typeName = request?.GetTypeName();

      _logger.LogInformation("Validating Request {RequestType}", typeName);

      var failures = _validators
          .Select(v => v.Validate(request))
          .SelectMany(result => result.Errors)
          .Where(error => error != null)
          .ToList();

      if (failures.Any())
      {
        var errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());

        _logger.LogWarning("Validation errors - {RequestType} - Command: {@Command} - Errors: {@ValidationErrors}", typeName, request, failures);
        throw new Exception("Validation Errors", new ValidationException(errors));
      }

      return await next();
    }
  }
}