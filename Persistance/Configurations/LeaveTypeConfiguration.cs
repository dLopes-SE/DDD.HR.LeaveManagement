using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations
{
  public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
  {
    public void Configure(EntityTypeBuilder<LeaveType> builder)
    {
      builder.HasData(
        new LeaveType
        {
          Id = 1,
          DateCreated = DateTime.Now,
          DateUpdated = DateTime.Now,
          Name = "Vacation", 
          DefaultDays = 10
        }
      );

      builder.Property(p => p.Name)
        .IsRequired()
        .HasMaxLength(100);
    }
  }
}
