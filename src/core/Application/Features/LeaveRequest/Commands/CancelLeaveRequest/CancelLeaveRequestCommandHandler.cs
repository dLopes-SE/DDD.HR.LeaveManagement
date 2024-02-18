using Application.Exceptions;
using Application.Features.LeaveRequest.Commands.CancelLeaveRequest;
using Application.Interfaces.Email;
using Application.Interfaces.Persistence;
using Application.Models;
using MediatR;

namespace Api.Controllers
{
  public class CancelLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepo, IEmailSender emailSender) : IRequestHandler<CancelLeaveRequestCommand, Unit>
  {
    public async Task<Unit> Handle(CancelLeaveRequestCommand request, CancellationToken cancellationToken)
    {
      var leaveRequest = await leaveRequestRepo.GetByIdAsync(request.Id);
      if (leaveRequest is null)  
        throw new NotFoundException(nameof(leaveRequest), request.Id);

      leaveRequest.Cancelled = true;
      await leaveRequestRepo.UpdateAsync(leaveRequest);

      // if already approved, re-evaluate the employee's allocations for the leave type

      // send confirmation email
      var email = new EmailMessage
      {
        To = string.Empty, /* Get email from employee record */
        Body = $"Your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} " +
                  $"has been cancelled successfully.",
        Subject = "Leave Request Cancelled"
      };

      await emailSender.SendEmail(email);
      return Unit.Value;
    }
  }
}
