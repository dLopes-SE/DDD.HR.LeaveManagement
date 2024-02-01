using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Features.LeaveType.Command.CreateLeaveType
{
  public class CreateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository) : IRequestHandler<CreateLeaveTypeCommand, int>
  {
    private readonly IMapper _mapper = mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepo = leaveTypeRepository;

    public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
      // Validate data

      var data = _mapper.Map<Domain.LeaveType>(request);

      return (await _leaveTypeRepo.CreateAsync(data)).Id;
    }
  }
}
