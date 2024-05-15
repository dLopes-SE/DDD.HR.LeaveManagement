using Domain;
using Microsoft.EntityFrameworkCore;
using Persistance.DataBaseContext;
using System.Text.Json;

namespace IntegrationTests
{
  public class HrDatabaseContextFixture : IDisposable
  {
    public HrDbContext DbContext { get; private set; }

    public HrDatabaseContextFixture()
    {
      var dbOptions = new DbContextOptionsBuilder<HrDbContext>()
        .UseInMemoryDatabase(new Guid().ToString())
        .Options;

      DbContext = new HrDbContext(dbOptions);

      var leaveTypes = JsonSerializer.Deserialize<List<LeaveType>>(File.ReadAllText("../../../../LeaveTypesData.json"));

      // Clean table 
      DbContext.LeaveTypes.RemoveRange(DbContext.LeaveTypes);
      DbContext.SaveChanges();

      // Add sample data
      DbContext.LeaveTypes.AddRange(leaveTypes);
      DbContext.SaveChanges();
    }

    public void Dispose()
    {
      DbContext.Dispose();
    }
  }
}
