using Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Features.LeaveType.Queries.GetLeaveTypeDetails
{
  public class GetLeaveTypeDetailsHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository) : IRequestHandler<LeaveTypeDetailsQuery, LeaveTypeDetailsDto>
  {
    private IMapper _mapper = mapper;
    private ILeaveTypeRepository _leaveTypeRepo = leaveTypeRepository;

    public async Task<LeaveTypeDetailsDto> Handle(LeaveTypeDetailsQuery request, CancellationToken cancellationToken, int id)
    {
      var leaveType = await _leaveTypeRepo.GetByIdAsync(request.Id);

      return _mapper.Map<LeaveTypeDetailsDto>(leaveType);
    }
  }
}
