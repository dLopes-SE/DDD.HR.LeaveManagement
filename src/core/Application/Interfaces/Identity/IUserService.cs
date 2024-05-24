using Application.Models.Identity;

namespace Application.Interfaces.Identity
{
  public interface IUserService
  {
    Task<List<Employee>> GetEmployees();
    Task<Employee> GetEmployeeById(int id);
  }
}
