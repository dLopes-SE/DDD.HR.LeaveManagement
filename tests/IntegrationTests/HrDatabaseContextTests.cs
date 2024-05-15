using Domain;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTests
{
  public class HrDatabaseContextTests : IClassFixture<HrDatabaseContextFixture>
  {
    private readonly HrDatabaseContextFixture _dbFixture;
    public HrDatabaseContextTests(HrDatabaseContextFixture dbFixture)
    {
      _dbFixture = dbFixture;
    }

    [Fact]
    public async Task Save_SetDateCreatedAndDateUpdatedValuesAsync()
    {
      // Arrange
      var leaveType = new LeaveType
      {
        Id = 4,
        DefaultDays = 10,
        Name = "Test Vacation 4"
      };

      var list = await _dbFixture.DbContext.LeaveTypes.ToListAsync();

      // Act
      await _dbFixture.DbContext.LeaveTypes.AddAsync(leaveType);
      await _dbFixture.DbContext.SaveChangesAsync();

      // Assert
      Assert.True(leaveType.DateCreated != default);
      Assert.True(leaveType.DateUpdated != default);
    }

    [Fact]
    public async Task GivenThatLeaveTypeIsNotPresent_WhenLeaveTypeAdded_ThenTableMustHaveCount4()
    {
      // Arrange
      var leaveType = new LeaveType
      {
        Id = 5,
        DefaultDays = 10,
        Name = "Test Vacation 5"
      };

      // Act
      await _dbFixture.DbContext.LeaveTypes.AddAsync(leaveType);
      await _dbFixture.DbContext.SaveChangesAsync();

      // Assert
      var count = await _dbFixture.DbContext.LeaveTypes.CountAsync();
      Assert.Equal(4, count);
    }

    [Fact]
    public async Task GivenThatLeaveTypeTableHas5Records_WhenLeaveTypeDeleted_ThenTableMustHaveCount4()
    {
      // Arrange
      var leaveType = await _dbFixture.DbContext.LeaveTypes.FirstOrDefaultAsync(q => q.Id == 1);

      // Act
      _dbFixture.DbContext.LeaveTypes.Remove(leaveType);
      await _dbFixture.DbContext.SaveChangesAsync();

      // Assert
      var count = await _dbFixture.DbContext.LeaveTypes.CountAsync();
      Assert.Equal(4, count);
    }
  }
}