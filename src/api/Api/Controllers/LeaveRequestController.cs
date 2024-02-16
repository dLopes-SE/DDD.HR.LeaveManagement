using Application.Features.LeaveRequest;
using Application.Features.LeaveRequest.GetLeaveRequestDetail;
using Application.Features.LeaveRequest.GetLeaveRequestList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("/api/[controller]")]
  [ApiController]
  public class LeaveRequestController(IMediator mediator) : Controller
  {
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    public async Task<ActionResult<LeaveRequestListDto>> Get()
    {
      var leaveRequests = await mediator.Send(new GetLeaveRequestListQuery());
      if (leaveRequests.Count == 0)
        return NoContent();

      return Ok(leaveRequests);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<LeaveRequestDetailDto>> Get(int id)
    {
      var leaveRequest = await mediator.Send(new GetLeaveRequestDetailQuery() { Id = id });
      return Ok(leaveRequest);
    }
  }
}
