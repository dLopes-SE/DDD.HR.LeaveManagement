using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance.DataBaseContext;
using Persistance.Repositories;

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

      services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
      services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
      services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();
      services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();

      return services;
    }
  }
}
