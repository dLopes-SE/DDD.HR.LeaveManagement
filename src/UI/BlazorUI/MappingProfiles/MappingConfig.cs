using AutoMapper;
using BlazorUI.Models;
using BlazorUI.Services.Base;

namespace BlazorUI.MappingProfiles
{
  public class MappingConfig : Profile
  {
    public MappingConfig()
    {
      CreateMap<LeaveTypeDto, LeaveTypeVM>().ReverseMap();
      CreateMap<CreateLeaveTypeCommand, LeaveTypeVM>().ReverseMap();
      CreateMap<UpdateLeaveTypeCommand, LeaveTypeVM>().ReverseMap();
    }
  }
}
