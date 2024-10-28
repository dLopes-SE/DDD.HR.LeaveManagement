using BlazorUI.Contracts;
using BlazorUI.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorUI.Pages.LeaveTypes;

public partial class Details
{
  [Inject]
  ILeaveTypeService _client { get; set; }

  [Parameter]
  public int Id { get; set; }
  LeaveTypeVM leaveType = new();
  protected async override Task OnParametersSetAsync()
  {
    leaveType = await _client.GetLeaveTypeDetails(Id);
  }
}
