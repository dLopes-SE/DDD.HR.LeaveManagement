using Application.Interfaces.Identity;
using Application.Models.Identity;
using Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Identity.Services
{
  public class UserService : IUserService
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _contextAccessor;

    public UserService(UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor)
    {
      _userManager = userManager;
      _contextAccessor = contextAccessor;
    }

    public string UserId => _contextAccessor.HttpContext?.User?.FindFirstValue("uid");
    public async Task<List<Employee>> GetEmployees()
    {
      var users = await _userManager.GetUsersInRoleAsync("Employee");

      return users.Select(u => new Employee()
      {
        Id = u.Id,
        FirstName = u.FirstName,
        LastName = u.LastName,
        Email = u.Email
      }).ToList();
    }

    public async Task<Employee> GetEmployeeById(string id)
    {
      var user = await _userManager.FindByIdAsync(id);

      return new Employee()
      {
        Id = user.Id,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Email = user.Email
      };
    }

    public Task<Employee> GetEmployeeById(int id)
    {
      throw new NotImplementedException();
    }
  }
}
