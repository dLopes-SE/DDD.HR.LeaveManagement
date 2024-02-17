using Application.Exceptions;
using Application.Interfaces.Persistence;
using MediatR;

namespace Application.Features.LeaveRequest.Commands.DeleteLeaveRequest
{
  public class DeleteLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepo) : IRequestHandler<DeleteLeaveRequestCommand, Unit>
  {
    public async Task<Unit> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
    {
      var leaveRequest = await leaveRequestRepo.GetByIdAsync(request.Id);

      if (leaveRequest is not null)
      {
        await leaveRequestRepo.DeleteAsync(leaveRequest);
        return Unit.Value;
      }

      throw new NotFoundException(nameof(leaveRequest), request.Id);
    }
  }
}
