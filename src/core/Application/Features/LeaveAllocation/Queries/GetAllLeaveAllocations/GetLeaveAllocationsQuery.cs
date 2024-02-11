using MediatR;

namespace Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations
{
  public class GetLeaveAllocationsQuery : IRequest<List<LeaveAllocationDto>>;
}
