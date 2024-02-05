using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistance.DataBaseContext;

namespace Persistance.Repositories
{
  public class LeaveAllocationRepository(HrDbContext context) : GenericRepository<LeaveAllocation>(context), ILeaveAllocationRepository
  {
    public async Task AddAlocations(List<LeaveAllocation> allocations)
    {
      await _context.AddRangeAsync(allocations);
      await _context.SaveChangesAsync();
    }

    public async Task<bool> AllocationExists(string userId, int leaveTypeId, int period)
    {
      return await _context.LeaveAllocations.AsNoTracking()
        .AnyAsync(q => q.EmployeeId == userId
                    && q.LeaveTypeId == leaveTypeId
                    && q.Period == period);
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
    {
      return await _context.LeaveAllocations.AsNoTracking()
        .Include(q => q.LeaveType)
        .ToListAsync();
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId)
    {
      return await _context.LeaveAllocations.AsNoTracking()
              .Include(q => q.LeaveType)
              .Where(q => q.EmployeeId == userId)
              .ToListAsync();
    }

    public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
    {
      return await _context.LeaveAllocations.AsNoTracking()
        .Include(q => q.LeaveType)
        .FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId)
    {
      return await _context.LeaveAllocations.AsNoTracking()
        .FirstOrDefaultAsync(q => q.EmployeeId == userId && q.LeaveTypeId == leaveTypeId);
    }
  }
}