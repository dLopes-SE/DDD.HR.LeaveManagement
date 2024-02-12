using Application.Exceptions;
using Application.Interfaces.Persistence;
using MediatR;

namespace Application.Features.LeaveAllocation.Command.DeleteLeaveAllocation
{
  public class DeleteLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepo) : IRequestHandler<DeleteLeaveAllocationCommand, Unit>
  {
    public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
      var leaveAllocation = await leaveAllocationRepo.GetByIdAsync(request.Id);

      if (leaveAllocation is not null)
      {
        await leaveAllocationRepo.DeleteAsync(leaveAllocation);
        return Unit.Value;
      }

      throw new NotFoundException(nameof(leaveAllocation), request.Id);
    }
  }
}
