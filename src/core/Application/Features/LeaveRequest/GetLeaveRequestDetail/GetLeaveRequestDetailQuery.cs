using MediatR;

namespace Application.Features.LeaveRequest.GetLeaveRequestDetail
{
  public class GetLeaveRequestDetailQuery : IRequest<LeaveRequestDetailDto>
  {
    public int Id { get; set; }
  }
}
