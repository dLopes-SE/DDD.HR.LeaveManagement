using Application.Interfaces.Logging;
using Infrastructure.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace Infrastructure
{
  public static class DependencyInjection
  {
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configs)
    {
      // We should add here DI for the email sender service
      services.AddSingleton(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

      return services;
    } 
  }
}
