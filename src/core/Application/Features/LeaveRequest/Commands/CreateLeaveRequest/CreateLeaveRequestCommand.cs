using Application.DTOs.LeaveRequest;
using MediatR;

namespace Application.Features.LeaveRequest.Commands.CreateLeaveRequest
{
  public class CreateLeaveRequestCommand : BaseLeaveRequest, IRequest<int>
  {
    public string RequestComments { get; set; } = string.Empty;
  }
}
