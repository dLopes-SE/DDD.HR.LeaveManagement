using MediatR;

namespace Application.Features.LeaveType.Command.UpdateLeaveType
{
  public class UpdateLeaveTypeCommand : IRequest<Unit>
  {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int DefaultDays { get; set; }
  }
}
