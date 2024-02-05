using Application.Interfaces;
using Domain;
using Persistance.DataBaseContext;

namespace Persistance.Repositories
{
  public class LeaveAllocationRepository(HrDbContext context) : GenericRepository<LeaveAllocation>(context), ILeaveAllocationRepository
  {

  }
}