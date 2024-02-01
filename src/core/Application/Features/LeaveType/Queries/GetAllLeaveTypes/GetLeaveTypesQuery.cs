using MediatR;

namespace Application.Features.LeaveType.Queries.GetAllLeaveTypes
{
  public record GetLeaveTypesQuery : IRequest<List<LeaveTypeDto>>;
}
