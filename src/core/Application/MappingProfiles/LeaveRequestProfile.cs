using Application.Features.LeaveRequest.GetLeaveRequestDetail;
using Application.Features.LeaveRequest.GetLeaveRequestList;
using AutoMapper;
using Domain;

namespace Application.MappingProfiles
{
    public class LeaveRequestProfile : Profile
  {
    public LeaveRequestProfile()
    {
      CreateMap<LeaveRequestListDto, LeaveRequest>().ReverseMap();
      CreateMap<LeaveRequestDetailDto, LeaveRequest>();
    }
  }
}
