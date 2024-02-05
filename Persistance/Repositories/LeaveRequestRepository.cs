using Application.Interfaces;
using Domain;
using Persistance.DataBaseContext;

namespace Persistance.Repositories
{
  public class LeaveRequestRepository(HrDbContext context) : GenericRepository<LeaveRequest>(context), ILeaveRequestRepository
  {

  }
}