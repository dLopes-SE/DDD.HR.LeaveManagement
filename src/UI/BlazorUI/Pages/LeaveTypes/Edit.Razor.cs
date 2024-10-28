using Blazored.Toast.Services;
using BlazorUI.Contracts;
using BlazorUI.Models;
using Microsoft.AspNetCore.Components;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace BlazorUI.Pages.LeaveTypes;
public partial class Edit
{
  [Inject]
  IToastService _toastService { get; set; }
  [Inject]
  NavigationManager _navManager { get; set; }
  [Inject]
  ILeaveTypeService _client { get; set; }

  [Parameter]
  public int Id { get; set; }
  public string Message { get; private set; }

  LeaveTypeVM leaveType = new();
  protected override async Task OnParametersSetAsync()
  {
    leaveType = await _client.GetLeaveTypeDetails(Id);
  }

  public async Task EditLeaveType()
  {
    var response = await _client.UpdateLeaveType(Id, leaveType);
    if (response.Success)
    {
      _toastService.ShowSuccess("Leave Type updated Successfully");
      _navManager.NavigateTo("/leavetypes");
    }

    Message = response.Message;
  }
}
