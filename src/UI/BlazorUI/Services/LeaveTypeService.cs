using AutoMapper;
using Blazored.LocalStorage;
using BlazorUI.Contracts;
using BlazorUI.Models.LeaveTypes;
using BlazorUI.Services.Base;

namespace BlazorUI.Services
{
    public class LeaveTypeService : BaseHttpService, ILeaveTypeService
  {
    private readonly IMapper _mapper;

    public LeaveTypeService(IClient client, IMapper mapper, ILocalStorageService localStorage) : base(client, localStorage)
    {
      _mapper = mapper;
    }

    public async Task<Response<int>> CreateLeaveType(LeaveTypeVM leaveType)
    {
      try
      {
        await AddBearerToken();
        var createLeaveTypeCommand = _mapper.Map<CreateLeaveTypeCommand>(leaveType);
        await _client.LeaveTypesPOSTAsync(createLeaveTypeCommand);
        return new Response<int> { Success = true };
      }
      catch (ApiException ex)
      {
        return ConvertApiExceptions<int>(ex);
      }
    }

    public async Task<Response<Guid>> DeleteLeaveType(int id)
    {
      try
      {
        await AddBearerToken();
        await _client.LeaveTypesDELETEAsync(id);
        return new Response<Guid> { Success = true };
      }
      catch (ApiException ex)
      {
        return ConvertApiExceptions<Guid>(ex);
      }
    }

    public async Task<List<LeaveTypeVM>> GetLeaveTypes()
    {
      try
      {
        await AddBearerToken();
        var leaveTypes = await _client.LeaveTypesAllAsync();
        return _mapper.Map<List<LeaveTypeVM>>(leaveTypes);
      }
      catch (ApiException ex)
      {
        if (ex.StatusCode is not 204)
        {
          // Log exception
        }
        // Else simply return an empty list of leave types

        return _mapper.Map<List<LeaveTypeVM>>(new List<LeaveTypeDto>());
      }
    }

    public async Task<LeaveTypeVM> GetLeaveTypeDetails(int id)
    {
      await AddBearerToken();
      var leaveType = await _client.LeaveTypesGETAsync(id);
      return _mapper.Map<LeaveTypeVM>(leaveType);
    }

    public async Task<Response<Guid>> UpdateLeaveType(int id, LeaveTypeVM leaveType)
    {
      try
      {
        await AddBearerToken();
        var updateLeaveTypeCommand = _mapper.Map<UpdateLeaveTypeCommand>(leaveType);
        await _client.LeaveTypesPUTAsync(updateLeaveTypeCommand);
        return new Response<Guid> { Success = true };
      }
      catch (ApiException ex)
      {
        return ConvertApiExceptions<Guid>(ex);
      }
    }
  }
}
