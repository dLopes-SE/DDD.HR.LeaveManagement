using MediatR;

namespace Application.Features.LeaveRequest.Queries.GetLeaveRequestList
{
  public class GetLeaveRequestListQuery : IRequest<List<LeaveRequestListDto>>
  {
    public bool IsLoggedInUser { get; set; } // Replace this by checking if the logged in user is an admin
  }
}
