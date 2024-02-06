using Application.Exceptions;
using Application.Interfaces.Logging;
using Application.Interfaces.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.LeaveType.Command.UpdateLeaveType
{
    public class UpdateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository, IAppLogger<UpdateLeaveTypeCommandHandler> logger) : IRequestHandler<UpdateLeaveTypeCommand, Unit>
  {
    private readonly IMapper _mapper = mapper;
    private readonly ILeaveTypeRepository _leaveTypeRequestRepository = leaveTypeRepository;
    private readonly IAppLogger<UpdateLeaveTypeCommandHandler> _logger = logger;

    public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
      var validator = new UpdateLeaveTypeCommandValidator(_leaveTypeRequestRepository);
      var result = await validator.ValidateAsync(request, cancellationToken);

      if (result.Errors.Count != 0)
      {
        _logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(LeaveType), request.Id);
        throw new BadRequestException("Invalid Leave type", result);
      }

      var data = _mapper.Map<Domain.LeaveType>(request);

      await _leaveTypeRequestRepository.UpdateAsync(data);

      return Unit.Value;
    }
  }
}
