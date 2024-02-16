using MediatR;

namespace Application.Features.LeaveRequest.GetLeaveRequestList
{
    public class GetLeaveRequestListQuery : IRequest<List<LeaveRequestListDto>>
    {
    }
}
