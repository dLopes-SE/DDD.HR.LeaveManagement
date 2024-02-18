using Application.Exceptions;
using Application.Interfaces.Email;
using Application.Interfaces.Persistence;
using Application.Models;
using AutoMapper;
using MediatR;

namespace Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval
{
  public class ChangeLeaveRequestApprovalCommandHandler(IMapper mapper,
    ILeaveTypeRepository leaveTypeRepo,
    ILeaveRequestRepository leaveRequestRepo,
    IEmailSender emailSender)
    : IRequestHandler<ChangeLeaveRequestApprovalCommand, Unit>
  {
    public async Task<Unit> Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
    { 
      var validator = new ChangeLeaveRequestApprovalCommandValidator();
      var validationResult = validator.Validate(request);
      if (validationResult.Errors.Count > 0) 
        throw new BadRequestException("Invalid LeaveRequest", validationResult);

      var leaveRequest = await leaveRequestRepo.GetByIdAsync(request.Id) ?? throw new NotFoundException(nameof(LeaveRequest), request.Id);

      // update leaveRequest
      leaveRequest.Approved = request.Approved;
      await leaveRequestRepo.UpdateAsync(leaveRequest);

      //if request is approved, get and update the employee's allocations

      // send confirmation email
      var email = new EmailMessage
      {
        To = string.Empty, /* Get email from employee record */
        Body = $"The approval status for your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} " +
                  $"has been updated.",
        Subject = "Leave Request Approval Status Updated"
      };

      await emailSender.SendEmail(email);

      return Unit.Value;
    }
  }
}
