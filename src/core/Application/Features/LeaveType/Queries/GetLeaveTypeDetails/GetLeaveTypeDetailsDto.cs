namespace Application.Features.LeaveType.Queries.GetLeaveTypeDetails
{
  public class LeaveTypeDetailsDto
  {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int DefautlDays { get; set; }
    public DateTime? DateCreated { get; set; }
    public DateTime? DateModified { get; set; }
  }
}
