using Application.Exceptions;
using Application.Interfaces.Identity;
using Application.Interfaces.Logging;
using Application.Interfaces.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.LeaveAllocation.Command.CreateLeaveAllocation
{
  public class CreateLeaveAllocationCommandHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocationRepo, ILeaveTypeRepository leaveTypeRepo, IUserService userService) : IRequestHandler<CreateLeaveAllocationCommand, Unit>
  {
    public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
      var validator = new CreateLeaveAllocationCommandValidator(leaveTypeRepo);
      var validationResult = await validator.ValidateAsync(request, cancellationToken);

      if (validationResult.Errors.Count > 0)
        throw new BadRequestException("Invalid LeaveAllocation", validationResult);

      // Get Leave type for allocations
      var leaveType = await leaveTypeRepo.GetByIdAsync(request.LeaveTypeId);

      // Get Employees
      var employees = await userService.GetEmployees();

      // Get Period
      var period = DateTime.Now.Year;

      // Assign Allocations IF an allocation doesn't already exist for period and leave type
      var allocations = new List<Domain.LeaveAllocation>();
      foreach (var emp in employees)
      {
        if (await leaveAllocationRepo.AllocationExists(emp.Id, request.LeaveTypeId, period))
          continue;

        var allocation = new Domain.LeaveAllocation()
        {
          DateCreated = DateTime.Now,
          EmployeeId = emp.Id,
          LeaveTypeId = request.LeaveTypeId,
          NumberOfDays = leaveType.DefaultDays,
          Period = period
        };

        allocations.Add(allocation);
      }

      if (allocations.Count > 0)
        await leaveAllocationRepo.AddAlocations(allocations);

      return Unit.Value;
    }
  }
}
