using Application.Exceptions;
using Application.Interfaces.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.LeaveType.Queries.GetLeaveTypeDetails
{
    public class GetLeaveTypeDetailsHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository) : IRequestHandler<GetLeaveTypeDetailsQuery, LeaveTypeDetailsDto>
  {
    private readonly IMapper _mapper = mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepo = leaveTypeRepository;

    public async Task<LeaveTypeDetailsDto> Handle(GetLeaveTypeDetailsQuery request, CancellationToken cancellationToken)
    {
      var leaveType = (await _leaveTypeRepo.GetByIdAsync(request.Id)) ?? throw new NotFoundException(nameof(LeaveType), request.Id);

      return _mapper.Map<LeaveTypeDetailsDto>(leaveType);
    }
  }
}
