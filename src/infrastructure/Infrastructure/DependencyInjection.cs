using Application.Interfaces.Email;
using Application.Interfaces.Logging;
using Infrastructure.EmailService;
using Infrastructure.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
  public static class DependencyInjection
  {
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configs)
    {
      // We should add here DI for the email sender service
      services.AddSingleton(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
      services.AddScoped<IEmailSender, EmailSender>();

      return services;
    } 
  }
}
