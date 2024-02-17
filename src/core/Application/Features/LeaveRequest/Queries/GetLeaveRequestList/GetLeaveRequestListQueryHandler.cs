using Application.Interfaces.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.LeaveRequest.Queries.GetLeaveRequestList
{
    public class GetLeaveRequestListQueryHandler(ILeaveRequestRepository leaveRequestRepo, IMapper mapper) : IRequestHandler<GetLeaveRequestListQuery, List<LeaveRequestListDto>>
    {
        public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListQuery request, CancellationToken cancellationToken)
        {
            var data = await leaveRequestRepo.GetAsync();
            return mapper.Map<List<LeaveRequestListDto>>(data);
        }
    }
}
