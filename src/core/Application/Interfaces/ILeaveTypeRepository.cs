using Domain;

namespace Application.Interfaces
{
  public interface ILeaveTypeRepository : IGenericRepository<LeaveType>
  {
    Task<bool> IsLeaveTypeUnique(string name);
  }
}