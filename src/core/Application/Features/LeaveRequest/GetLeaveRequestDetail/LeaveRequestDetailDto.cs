using Application.Features.LeaveType.Queries.GetAllLeaveTypes;

namespace Application.Features.LeaveRequest.GetLeaveRequestDetail
{
  public class LeaveRequestDetailDto
  {
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string RequestingEmployeeId { get; set; } = string.Empty;
    public LeaveTypeDto? LeaveType { get; set; }
    public int LeaveTypeId { get; set; }
    public DateTime DateRequested { get; set; }
    public string RequestComments { get; set; } = string.Empty;
    public DateTime? DateActioned { get; set; }
    public bool? Approved { get; set; }
    public bool Cancelled { get; set; }
  }
}
