using Application.Interfaces.Persistence;
using FluentValidation;

namespace Application.Features.LeaveAllocation.Command.UpdateLeaveAllocation
{
  public class UpdateLeaveAllocationCommandValidator : AbstractValidator<UpdateLeaveAllocationCommand>
  {
    private readonly ILeaveTypeRepository _leaveTypeRepo;
    private readonly ILeaveAllocationRepository _leaveAllocationRepo;

    public UpdateLeaveAllocationCommandValidator(ILeaveTypeRepository leaveTypeRepo, ILeaveAllocationRepository leaveAllocationRepo)
    {
      _leaveTypeRepo = leaveTypeRepo;
      _leaveAllocationRepo = leaveAllocationRepo;

      RuleFor(p => p.Id)
       .GreaterThan(0)
       .MustAsync(LeaveAllocationMustExist)
       .WithMessage("{PropertyName} does not exist."); 
      
      RuleFor(p => p.LeaveTypeId)
        .GreaterThan(0)
        .MustAsync(LeaveTypeMustExist)
        .WithMessage("{PropertyName} does not exist.");

      RuleFor(p => p.NumberOfDays)
        .GreaterThan(0)
        .WithMessage("{PropertyName} must be greater than {ComparisonValue}");

      RuleFor(p => p.Period)
        .GreaterThanOrEqualTo(DateTime.Now.Year)
        .WithMessage("{PropertyName} must be after {Comparisonvalue}");
    }

    private async Task<bool> LeaveTypeMustExist(int id, CancellationToken cancellationToken)
    {
      var leaveType = await _leaveTypeRepo.GetByIdAsync(id);
      return leaveType is not null;
    }

    private async Task<bool> LeaveAllocationMustExist(int id, CancellationToken cancellationToken)
    {
      var leaveAllocation = await _leaveAllocationRepo.GetByIdAsync(id);
      return leaveAllocation is not null;
    }
  }
}
