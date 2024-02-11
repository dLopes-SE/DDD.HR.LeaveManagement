using Application.Interfaces.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations
{
  public class GetLeaveAllocationQueryHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocationRepo) : IRequestHandler<GetLeaveAllocationsQuery, List<LeaveAllocationDto>>
  {
    public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationsQuery request, CancellationToken cancellationToken)
    {
      var leaveAllocations = await leaveAllocationRepo.GetAsync();
      return mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);
    }
  }
}
