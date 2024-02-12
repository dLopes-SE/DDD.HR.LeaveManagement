using MediatR;

namespace Application.Features.LeaveAllocation.Command.DeleteLeaveAllocation
{
  public class DeleteLeaveAllocationCommand : IRequest<Unit>
  {
    public int Id { get; set; }
  }
}
