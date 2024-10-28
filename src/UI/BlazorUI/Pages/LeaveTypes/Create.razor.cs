using BlazorUI.Models;
using Microsoft.AspNetCore.Components;
using Blazored.Toast.Services;
using BlazorUI.Contracts;

namespace BlazorUI.Pages.LeaveTypes;

public partial class Create
{
  [Inject]
  IToastService _toastService { get; set; }
  [Inject]
  NavigationManager _navManager { get; set; }
  [Inject]
  ILeaveTypeService _client { get; set; }
  public string Message { get; private set; }

  LeaveTypeVM leaveType = new();
  public async Task CreateLeaveType()
  {
    var response = await _client.CreateLeaveType(leaveType);
    if (response.Success)
    {
      _toastService.ShowSuccess("Leave Type created Successfully");
      _toastService.ShowToast(ToastLevel.Info, "Test");
      _navManager.NavigateTo("/leavetypes");
    }

    Message = response.Message;
  }
}
