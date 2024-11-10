using Application.Exceptions;
using Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using Application.Interfaces.Persistence;
using AutoMapper;
using MediatR;

namespace Api.Controllers
{
  public class CreateLeaveRequestCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepo, ILeaveRequestRepository leaveRequestRepo) : IRequestHandler<CreateLeaveRequestCommand, Unit>
  {
    public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
      var validator = new CreateLeaveRequestCommandValidator(leaveTypeRepo);
      var validationResult = await validator.ValidateAsync(request, cancellationToken);

      if (validationResult.Errors.Count > 0)
        throw new BadRequestException("Invalid LeaveRequest", validationResult);

      var data = mapper.Map<Domain.LeaveRequest>(request);

      await leaveRequestRepo.CreateAsync(data);

      // Get requestiing employee's id

      // Check on employee's allocation

      // if allocations aren't enough, return validation error with message

      // Create leave request

      // Send email (which we haven't implemented)

      return Unit.Value;
    }
  }
}
