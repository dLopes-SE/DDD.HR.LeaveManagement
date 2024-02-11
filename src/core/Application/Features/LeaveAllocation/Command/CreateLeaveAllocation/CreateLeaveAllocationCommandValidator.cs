using Application.Interfaces.Persistence;
using FluentValidation;

namespace Application.Features.LeaveAllocation.Command.CreateLeaveAllocation
{
  public class CreateLeaveAllocationCommandValidator : AbstractValidator<CreateLeaveAllocationCommand>
  {
    private readonly ILeaveAllocationRepository _leaveTypeRepo;

    public CreateLeaveAllocationCommandValidator(ILeaveAllocationRepository leaveTypeRepo)
    {
      _leaveTypeRepo = leaveTypeRepo;

      RuleFor(p => p.LeaveTypeId)
                .GreaterThan(0)
                .MustAsync(LeaveTypeMustExist)
                .WithMessage("{PropertyName} does not exist.");
    }
    private async Task<bool> LeaveTypeMustExist(int id, CancellationToken cancellationToken)
    {
      var leaveType = await _leaveTypeRepo.GetByIdAsync(id);
      return leaveType is not null;
    }
  }
}
