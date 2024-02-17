using MediatR;

namespace Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail
{
    public class GetLeaveRequestDetailQuery : IRequest<LeaveRequestDetailDto>
    {
        public int Id { get; set; }
    }
}
