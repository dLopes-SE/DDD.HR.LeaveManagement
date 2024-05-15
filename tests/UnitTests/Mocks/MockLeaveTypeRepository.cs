using Application.Interfaces.Persistence;
using Domain;
using Moq;
using System.Text.Json;

namespace UnitTests.Mocks
{
  public class MockLeaveTypeRepository
  {
    public static Mock<ILeaveTypeRepository> GetMockLeaveTypeRepository()
    {
      var leaveTypes = JsonSerializer.Deserialize<List<LeaveType>>(File.ReadAllText("../../../../LeaveTypesData.json"));

      var mockRepo = new Mock<ILeaveTypeRepository>();

      mockRepo.Setup(r => r.GetAsync())
        .ReturnsAsync(leaveTypes);

      mockRepo.Setup(r => r.CreateAsync(It.IsAny<LeaveType>()))
        .Returns((LeaveType leaveType) =>
        {
          leaveTypes.Add(leaveType);
          return Task.CompletedTask;
        });

      return mockRepo;
    }
  }
}