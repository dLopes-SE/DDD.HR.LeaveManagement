using Domain;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Persistance.DataBaseContext
{
  public class HrDbContext : DbContext
  {
    public HrDbContext(DbContextOptions<HrDbContext> options) : base(options)
    {
    }

    public DbSet<LeaveType> LeaveTypes { get; set; }
    public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
    public DbSet<LeaveRequest> LeaveRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfigurationsFromAssembly(typeof(HrDbContext).Assembly);
      base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
      foreach(var entry in base.ChangeTracker.Entries<BaseEntity>()
        .Where(q => q.State is EntityState.Added || q.State is EntityState.Modified))
      {
        entry.Entity.DateUpdated = DateTime.UtcNow;

        if (entry.State is EntityState.Added)
          entry.Entity.DateCreated = DateTime.Now;
      };

      return base.SaveChangesAsync(cancellationToken);
    }
  }
}
