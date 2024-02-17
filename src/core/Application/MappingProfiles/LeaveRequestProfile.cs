using Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;
using Application.Features.LeaveRequest.Queries.GetLeaveRequestList;
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
      CreateMap<CreateLeaveRequestCommand, LeaveRequest>();
      CreateMap<UpdateLeaveRequestCommand, LeaveRequest>();
    }
  }
}
