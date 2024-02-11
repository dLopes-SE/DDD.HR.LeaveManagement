using MediatR;

namespace Application.Features.LeaveAllocation.Command.CreateLeaveAllocation
{
  public class CreateLeaveAllocationCommand : IRequest<Unit>
  {
    public int LeaveTypeId { get; set; }
  }
}
