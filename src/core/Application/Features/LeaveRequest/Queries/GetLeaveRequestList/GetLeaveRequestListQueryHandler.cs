using Application.Interfaces.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.LeaveRequest.Queries.GetLeaveRequestList
{
  public class GetLeaveRequestListQueryHandler(ILeaveRequestRepository leaveRequestRepo, IMapper mapper) : IRequestHandler<GetLeaveRequestListQuery, List<LeaveRequestListDto>>
  {
    public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListQuery request, CancellationToken cancellationToken)
    {
      List<Domain.LeaveRequest> leaveRequests = new List<Domain.LeaveRequest>();
      var requests = new List<LeaveRequestListDto>();

      // Check if it is logged in employee

      // Fill requests with employee information

      return requests;
    }
  }
}
