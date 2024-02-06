using Application.Interfaces.Persistence;
using FluentValidation;

namespace Application.Features.LeaveType.Command.UpdateLeaveType
{
  public class UpdateLeaveTypeCommandValidator : AbstractValidator<UpdateLeaveTypeCommand>
  {
    private readonly ILeaveTypeRepository _leaveTypeRepo;
    public UpdateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
      _leaveTypeRepo = leaveTypeRepository;

      RuleFor(p => p.Id)
        .NotNull()
        .MustAsync(LeaveTypeMustExist);

      RuleFor(p => p.Name)
        .NotEmpty().WithMessage("{PropertyName} is required")
        .NotNull()
        .MaximumLength(70).WithMessage("{PropertyName} must be under 70 characters)");

      RuleFor(p => p.DefaultDays)
        .LessThan(100).WithMessage("{PropertyName} cannot exceed 100")
        .GreaterThan(1).WithMessage("{PropertyName} cannot be less than 1");

      RuleFor(q => q)
        .MustAsync(LeaveTypeNameUnique).WithMessage("Leave type already exists");
    }

    private async Task<bool> LeaveTypeMustExist(int id, CancellationToken cancellationToken)
    {
      return await _leaveTypeRepo.GetByIdAsync(id) is not null;
    }

    private Task<bool> LeaveTypeNameUnique(UpdateLeaveTypeCommand command, CancellationToken cancellationToken)
    {
      return _leaveTypeRepo.IsLeaveTypeUnique(command.Name);
    }
  }
}
