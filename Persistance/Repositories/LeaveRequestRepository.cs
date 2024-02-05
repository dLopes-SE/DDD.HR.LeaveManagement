using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistance.DataBaseContext;

namespace Persistance.Repositories
{
  public class LeaveRequestRepository(HrDbContext context) : GenericRepository<LeaveRequest>(context), ILeaveRequestRepository
  {
    public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails()
    {
      return await _context.LeaveRequests.Include(q => q.LeaveType)
        .AsNoTracking()
        .ToListAsync();
    }

    public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(string userId)
    {
      return await _context.LeaveRequests.Where(q => q.RequestingEmployeeId == userId)
        .Include(q => q.LeaveType)
        .AsNoTracking()
        .ToListAsync(); 
    }

    public async Task<LeaveRequest> GetLeaveRequestWithDetails(int id)
    {
      return await _context.LeaveRequests
              .AsNoTracking()
              .Include(q => q.LeaveType)
              .FirstOrDefaultAsync(q => q.Id == id);
    }
  }
}