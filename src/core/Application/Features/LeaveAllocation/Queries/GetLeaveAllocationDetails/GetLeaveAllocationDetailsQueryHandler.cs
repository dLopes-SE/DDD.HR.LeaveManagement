using Application.Exceptions;
using Application.Interfaces.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;

public class GetLeaveAllocationDetailsQueryHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocationRepo) : IRequestHandler<GetLeaveAllocationDetailsQuery, LeaveAllocationDetailsDto>
{
  public async Task<LeaveAllocationDetailsDto> Handle(GetLeaveAllocationDetailsQuery request, CancellationToken cancellationToken)
  {
    var leaveAllocation = await leaveAllocationRepo.GetLeaveAllocationWithDetails(request.Id);

    return leaveAllocation is null
      ? throw new NotFoundException(nameof(LeaveAllocation), request.Id)
      : mapper.Map<LeaveAllocationDetailsDto>(leaveAllocation);
  }
}
