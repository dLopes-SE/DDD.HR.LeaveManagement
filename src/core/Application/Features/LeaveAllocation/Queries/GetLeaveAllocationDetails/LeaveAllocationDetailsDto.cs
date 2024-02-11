using Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

namespace Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;

public class LeaveAllocationDetailsDto
{
  public int Id { get; set; }
  public int NumberOfDays { get; set; }
  public int LeaveTypeId { get; set; }
  public LeaveTypeDetailsDto? LeaveType { get; set; }
  public int Period {  get; set; }
}
