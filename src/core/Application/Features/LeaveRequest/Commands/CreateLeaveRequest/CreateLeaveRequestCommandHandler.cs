using Application.Exceptions;
using Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using Application.Interfaces.Persistence;
using AutoMapper;
using MediatR;

namespace Api.Controllers
{
  public class CreateLeaveRequestCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepo, ILeaveRequestRepository leaveRequestRepo) : IRequestHandler<CreateLeaveRequestCommand, int>
  {
    public async Task<int> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
      var validator = new CreateLeaveRequestCommandValidator(leaveTypeRepo);
      var validationResult = await validator.ValidateAsync(request, cancellationToken);

      if (validationResult.Errors.Count > 0)
        throw new BadRequestException("Invalid LeaveRequest", validationResult);

      var data = mapper.Map<Domain.LeaveRequest>(request);

      await leaveRequestRepo.CreateAsync(data);

      return data.Id;
    }
  }
}
