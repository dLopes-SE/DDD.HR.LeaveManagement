using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance.DataBaseContext;

namespace Persistance
{
  public static class DependencyInjection
  {
    public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configs)
    {
      services.AddDbContext<HrDbContext>(options =>
      {
        options.UseSqlServer(configs.GetConnectionString("HrDBConnectionString"));
      });

      return services;
    }
  }
}
