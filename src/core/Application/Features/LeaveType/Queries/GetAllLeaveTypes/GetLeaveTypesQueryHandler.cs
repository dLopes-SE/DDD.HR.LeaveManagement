using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Features.LeaveType.Queries.GetAllLeaveTypes
{
  public class GetLeaveTypesQueryHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository) : IRequestHandler<GetLeaveTypesQuery, List<LeaveTypeDto>>
  {
    private readonly IMapper _mapper = mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepo = leaveTypeRepository;

    public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypesQuery request, CancellationToken cancellationToken)
    {
      // query the database
      var leaveTypes = await _leaveTypeRepo.GetAsync();

      // convert data object to DTO objects
      return _mapper.Map<List<LeaveTypeDto>>(leaveTypes);
    }
  }
}