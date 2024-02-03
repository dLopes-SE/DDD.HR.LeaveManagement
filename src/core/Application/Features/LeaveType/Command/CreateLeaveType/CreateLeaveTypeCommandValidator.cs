using Application.Interfaces;
using FluentValidation;

namespace Application.Features.LeaveType.Command.CreateLeaveType
{
  public class CreateLeaveTypeCommandValidator : AbstractValidator<CreateLeaveTypeCommand>
  {
    private readonly ILeaveTypeRepository _leaveTypeRepo;
    public CreateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
      _leaveTypeRepo = leaveTypeRepository;

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

    private Task<bool> LeaveTypeNameUnique (CreateLeaveTypeCommand command, CancellationToken cancellationToken)
    {
      return _leaveTypeRepo.IsLeaveTypeUnique(command.Name);
    }
  }
}
