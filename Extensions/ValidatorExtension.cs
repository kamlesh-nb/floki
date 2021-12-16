using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace Floki.Extensions
{
  public static class ValidatorExtension
  {
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
      services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
      return services;
    }
  }
}