namespace Application.DTOs.LeaveRequest
{
  public class BaseLeaveRequest
  {
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int LeaveTypeId { get; set; }
  }
}
