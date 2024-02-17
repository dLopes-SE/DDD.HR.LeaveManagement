using Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using Application.Features.LeaveRequest.Shared;
using Application.Interfaces.Persistence;
using FluentValidation;

namespace Application.Features.LeaveRequest.Commands.UpdateLeaveRequest
{
  public class UpdateLeaveRequestCommandValidator : AbstractValidator<UpdateLeaveRequestCommand>
  {
    private readonly ILeaveTypeRepository _leaveTypeRepo;
    private readonly ILeaveRequestRepository _leaveRequestRepo;

    public UpdateLeaveRequestCommandValidator(ILeaveTypeRepository leaveTypeRepo, ILeaveRequestRepository leaveRequestRepo)
    {
      _leaveTypeRepo = leaveTypeRepo;
      _leaveRequestRepo = leaveRequestRepo;
      Include(new BaseLeaveRequestValidator(leaveTypeRepo));

      RuleFor(p => p.Id)
        .NotNull()
        .MustAsync(LeaveRequestMustExist)
        .WithMessage("{Property} must be present");
    }

    private async Task<bool> LeaveRequestMustExist(int id, CancellationToken cancellationToken)
    {
      var leaveRequest = await _leaveRequestRepo.GetByIdAsync(id);
      return leaveRequest is not null;
    }
  }
}
