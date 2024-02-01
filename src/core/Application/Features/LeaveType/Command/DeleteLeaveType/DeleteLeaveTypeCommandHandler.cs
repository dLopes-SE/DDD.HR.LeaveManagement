using Application.Interfaces;
using MediatR;

namespace Application.Features.LeaveType.Command.DeleteLeaveType
{
  public class DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository) : IRequestHandler<DeleteLeaveTypeCommand, Unit>
  {
    private readonly ILeaveTypeRepository _leaveTypeRepo = leaveTypeRepository;

    public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
    {
      var leaveType = await _leaveTypeRepo.GetByIdAsync(request.Id);

      if (leaveType is not null)
      {
        await _leaveTypeRepo.DeleteAsync(leaveType);
      }

      return Unit.Value;
    }
  }
}
