using Blazored.LocalStorage;
using BlazorUI.Contracts;
using BlazorUI.Services.Base;

namespace BlazorUI.Services
{
  public class LeaveAllocationService : BaseHttpService, ILeaveAllocationService
  {
    public LeaveAllocationService(IClient client, ILocalStorageService localStorage) : base(client, localStorage)
    {
    }

    public async Task<Response<Guid>> CreateLeaveAllocations(int leaveTypeId)
    {
      try
      {
        var createLeaveAllocationCommand = new CreateLeaveAllocationCommand { LeaveTypeId = leaveTypeId };
        await _client.LeaveAllocationsPOSTAsync(createLeaveAllocationCommand);

        return new Response<Guid>();
      }
      catch (ApiException ex)
      {
        return ConvertApiExceptions<Guid>(ex);
      }
    }
  }
}
