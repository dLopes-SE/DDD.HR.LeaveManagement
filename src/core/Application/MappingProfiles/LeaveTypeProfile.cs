﻿using Application.Features.LeaveType.Command.CreateLeaveType;
using Application.Features.LeaveType.Command.UpdateLeaveType;
using Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using AutoMapper;
using Domain;

namespace Application.MappingProfiles
{
  public class LeaveTypeProfile : Profile
  {
    public LeaveTypeProfile()
    {
      CreateMap<LeaveTypeDto, LeaveType>().ReverseMap();
      CreateMap<LeaveType, LeaveTypeDetailsDto>();
      CreateMap<CreateLeaveTypeCommand, LeaveType>();
      CreateMap<UpdateLeaveTypeCommand, LeaveType>();
    }
  }
}
