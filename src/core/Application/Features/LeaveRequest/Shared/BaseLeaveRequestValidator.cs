﻿using Application.DTOs.LeaveRequest;
using Application.Interfaces.Persistence;
using FluentValidation;

namespace Application.Features.LeaveRequest.Shared
{
    public class BaseLeaveRequestValidator : AbstractValidator<BaseLeaveRequest>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepo;

        public BaseLeaveRequestValidator(ILeaveTypeRepository leaveTypeRepo)
        {
            _leaveTypeRepo = leaveTypeRepo;

            RuleFor(p => p.StartDate)
                      .LessThan(p => p.EndDate).WithMessage("{PropertyName} must be before {ComparisonValue}");

            RuleFor(p => p.EndDate)
                .GreaterThan(p => p.StartDate).WithMessage("{PropertyName} must be after {ComparisonValue}");

            RuleFor(p => p.LeaveTypeId)
                .GreaterThan(0)
                .MustAsync(LeaveTypeMustExist)
                .WithMessage("{PropertyName} does not exist.");

        }

        private async Task<bool> LeaveTypeMustExist(int id, CancellationToken cancellationToken)
        {
            var leaveType = await _leaveTypeRepo.GetByIdAsync(id);
            return leaveType is not null;
        }
    }
}
