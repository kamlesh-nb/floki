using System.Threading;
using System.Threading.Tasks;
using Floki.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Floki.Behaviours
{
  public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
  {
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> logger;
    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> _logger)
    {
        logger = _logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        logger.LogInformation("[REQUEST] {requestName} ({@request})", request?.GetTypeName(), request);
        var response = await next();
        logger.LogInformation("[RESPONSE] {requestName} handled - response: {@response}", request?.GetTypeName(), response);

        return response;
    }
  }
}