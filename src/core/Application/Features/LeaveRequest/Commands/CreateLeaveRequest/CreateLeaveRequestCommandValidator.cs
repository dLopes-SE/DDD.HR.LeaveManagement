using Application.Features.LeaveRequest.Shared;
using Application.Interfaces.Persistence;
using FluentValidation;

namespace Application.Features.LeaveRequest.Commands.CreateLeaveRequest
{
  public class CreateLeaveRequestCommandValidator : AbstractValidator<CreateLeaveRequestCommand>
  {
    private readonly ILeaveTypeRepository _leaveTypeRepo;

    public CreateLeaveRequestCommandValidator(ILeaveTypeRepository leaveTypeRepo)
    {
      _leaveTypeRepo = leaveTypeRepo; 
      Include(new BaseLeaveRequestValidator(leaveTypeRepo));
    }
  }
}
