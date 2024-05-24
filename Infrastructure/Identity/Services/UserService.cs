using Application.Exceptions;
using Application.Interfaces.Identity;
using Application.Models.Identity;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography.Xml;

namespace Identity.Services
{
  public class UserService : IUserService
  {
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(UserManager<ApplicationUser> userManager)
    {
      _userManager = userManager;
    }

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
  }
}
