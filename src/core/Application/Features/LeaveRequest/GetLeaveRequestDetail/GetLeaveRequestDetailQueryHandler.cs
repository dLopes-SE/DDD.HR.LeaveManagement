using Application.Exceptions;
using Application.Interfaces.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.LeaveRequest.GetLeaveRequestDetail
{
  public class GetLeaveRequestDetailQueryHandler(IMapper mapper, ILeaveRequestRepository leaveRequestRepo) : IRequestHandler<GetLeaveRequestDetailQuery, LeaveRequestDetailDto>
  {
    public async Task<LeaveRequestDetailDto> Handle(GetLeaveRequestDetailQuery request, CancellationToken cancellationToken)
    {
      var leaveRequest = mapper.Map<LeaveRequestDetailDto>(await leaveRequestRepo.GetLeaveRequestWithDetails(request.Id));
      if (leaveRequest == null)
        throw new NotFoundException(nameof(leaveRequest), request.Id);

      // Add Employee details as needed
      return leaveRequest;
    }
  }
}
