using Application.Exceptions;
using Application.Interfaces.Logging;
using Application.Interfaces.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.LeaveAllocation.Command.CreateLeaveAllocation
{
  public class CreateLeaveAllocationCommandHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocationRepo, ILeaveTypeRepository leaveTypeRepo) : IRequestHandler<CreateLeaveAllocationCommand, Unit>
  {
    public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
      var validator = new CreateLeaveAllocationCommandValidator(leaveAllocationRepo);
      var validationResult = await validator.ValidateAsync(request, cancellationToken);

      if (validationResult.Errors.Count > 0)
        throw new BadRequestException("Invalid LeaveAllocation", validationResult);

      // Get Leave type for allocations
      var leaveType = await leaveTypeRepo.GetByIdAsync(request.LeaveTypeId);

      // Get Employees

      //Get Period

      // SaveToDB
      var data = mapper.Map<Domain.LeaveAllocation>(request);
      await leaveAllocationRepo.CreateAsync(data);
      return Unit.Value;
    }
  }
}
