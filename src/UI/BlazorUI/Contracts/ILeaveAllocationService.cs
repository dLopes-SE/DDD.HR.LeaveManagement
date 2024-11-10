using BlazorUI.Models.LeaveAllocations;
using BlazorUI.Services.Base;

namespace BlazorUI.Contracts;
public interface ILeaveAllocationService
{
  public Task<Response<Guid>> CreateLeaveAllocations(int leaveTypeId);
}
