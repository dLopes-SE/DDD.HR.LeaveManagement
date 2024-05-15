using Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using Application.Interfaces.Persistence;
using Application.MappingProfiles;
using AutoMapper;
using Moq;
using UnitTests.Mocks;

namespace UnitTests.Features.LeaveTypes.Queries
{
  public class GetLeaveTypeListQueryHandlerTests
  {
    private readonly Mock<ILeaveTypeRepository> _mockRepo;
    private readonly IMapper _mapper;

    public GetLeaveTypeListQueryHandlerTests()
    {
      _mockRepo = MockLeaveTypeRepository.GetMockLeaveTypeRepository();

      var mapperConfig = new MapperConfiguration(c =>
      {
        c.AddProfile<LeaveTypeProfile>();
      });

      _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task GetLeaveTypes_ShouldReturnThreeObjects()
    {
      var handler = new GetLeaveTypesQueryHandler(_mapper, _mockRepo.Object);
      var result = await handler.Handle(new GetLeaveTypesQuery(), CancellationToken.None);

      Assert.NotNull(result);
      Assert.IsType<List<LeaveTypeDto>>(result);
      Assert.True(result.Count == 3);
    }
  }
}
