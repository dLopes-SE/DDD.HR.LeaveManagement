using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Features.LeaveType.Command.CreateLeaveType
{
  public class CreateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository) : IRequestHandler<CreateLeaveTypeCommand, int>
  {
    private readonly IMapper _mapper = mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepo = leaveTypeRepository;

    public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
      // Validate data
      var validator = new CreateLeaveTypeCommandValidator(_leaveTypeRepo);
      var validationResult = await validator.ValidateAsync(request, cancellationToken);

      if (validationResult.Errors.Count > 0)
        throw new BadRequestException("Invalid LeaveType", validationResult);

      var data = _mapper.Map<Domain.LeaveType>(request);

      await _leaveTypeRepo.CreateAsync(data);

      return data.Id;
    }
  }
}
