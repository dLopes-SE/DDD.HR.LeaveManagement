using MediatR;

namespace Application.Features.LeaveType.Queries.GetLeaveTypeDetails
{
  public record LeaveTypeDetailsQuery(int Id) : IRequest<LeaveTypeDetailsDto>;
}