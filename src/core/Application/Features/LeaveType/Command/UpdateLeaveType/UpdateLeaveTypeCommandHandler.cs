using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Features.LeaveType.Command.UpdateLeaveType
{
  public class UpdateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository) : IRequestHandler<UpdateLeaveTypeCommand, Unit>
  {
    private readonly IMapper _mapper = mapper;
    private readonly ILeaveTypeRepository _leaveTypeRequestRepository = leaveTypeRepository;

    public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
      // Validate data
      var data = _mapper.Map<Domain.LeaveType>(request);

      await _leaveTypeRequestRepository.UpdateAsync(data);

      return Unit.Value;
    }
  }
}
