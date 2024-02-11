using Application.Exceptions;
using Application.Interfaces.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.LeaveAllocation.Command.UpdateLeaveAllocation
{
  public class UpdateLeaveAllocationCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepo, ILeaveAllocationRepository leaveAllocationRepo) : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
  {
    public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
      var validator = new UpdateLeaveAllocationCommandValidator(leaveTypeRepo, leaveAllocationRepo);
      var validationResult = await validator.ValidateAsync(request, cancellationToken);

      if (validationResult.Errors.Count != 0)
        throw new BadRequestException("Invalid LeaveAllocation", validationResult);

      var data = mapper.Map<Domain.LeaveAllocation>(request);
      await leaveAllocationRepo.UpdateAsync(data);

      return Unit.Value;
    }
  }
}
