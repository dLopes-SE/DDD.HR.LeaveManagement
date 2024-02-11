using Application.Features.LeaveAllocation.Command.CreateLeaveAllocation;
using Application.Features.LeaveAllocation.Command.UpdateLeaveAllocation;
using Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations;
using Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using AutoMapper;
using Domain;

namespace Application.MappingProfiles
{
  public class LeaveAllocationProfile : Profile
  {
    public LeaveAllocationProfile()
    {
      CreateMap<LeaveAllocationDto, LeaveAllocation>().ReverseMap();
      CreateMap<LeaveAllocation, LeaveAllocationDetailsDto>();
      CreateMap<LeaveAllocation, CreateLeaveAllocationCommand>();
      CreateMap<LeaveAllocation, UpdateLeaveAllocationCommand>();
    }
  }
}
