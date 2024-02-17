using Application.Exceptions;
using Application.Interfaces.Persistence;
using AutoMapper;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.LeaveRequest.Commands.UpdateLeaveRequest
{
  public class UpdateLeaveRequestCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepo, ILeaveRequestRepository leaveRequestRepo) : IRequestHandler<UpdateLeaveRequestCommand, Unit>
  {
    public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
      var validator = new UpdateLeaveRequestCommandValidator(leaveTypeRepo, leaveRequestRepo);
      var validationResult = validator.Validate(request);

      if (validationResult.Errors.Count > 0)
        throw new BadRequestException("Invalid LeaveRequest", validationResult);

      var data = mapper.Map<Domain.LeaveRequest>(request);

      await leaveRequestRepo.UpdateAsync(data);

      return Unit.Value;
    }
  }
}
