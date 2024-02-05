using Application.Interfaces.Persistence;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistance.DataBaseContext;

namespace Persistance.Repositories
{
    public class LeaveTypeRepository(HrDbContext context) : GenericRepository<LeaveType>(context), ILeaveTypeRepository
  {
    public async Task<bool> IsLeaveTypeUnique(string name)
    {
      return await _context.LeaveTypes.AnyAsync(q => q.Name == name);
    }
  }
}