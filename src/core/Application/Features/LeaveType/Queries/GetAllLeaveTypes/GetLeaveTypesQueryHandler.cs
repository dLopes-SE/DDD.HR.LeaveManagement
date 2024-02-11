using Application.Interfaces.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.LeaveType.Queries.GetAllLeaveTypes
{
  public class GetLeaveTypesQueryHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository) : IRequestHandler<GetLeaveTypesQuery, List<LeaveTypeDto>>
  {
    public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypesQuery request, CancellationToken cancellationToken)
    {
      // query the database
      var leaveTypes = await leaveTypeRepository.GetAsync();

      // convert data object to DTO objects
      return mapper.Map<List<LeaveTypeDto>>(leaveTypes);
    }
  }
}