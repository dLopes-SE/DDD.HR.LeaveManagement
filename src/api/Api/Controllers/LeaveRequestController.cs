using Application.Features.LeaveRequest;
using Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;
using Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;
using Application.Features.LeaveRequest.Queries.GetLeaveRequestList;
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

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<int>> Create([FromBody] CreateLeaveRequestCommand leaveRequestObj)
    {
      var id = await mediator.Send(leaveRequestObj);
      return CreatedAtAction(nameof(Get), new { id });
    }

    [HttpPut]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> Update([FromBody] UpdateLeaveRequestCommand leaveRequestObj)
    {
      await mediator.Send(leaveRequestObj);
      return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> Delete(int id)
    {
      await mediator.Send(new DeleteLeaveRequestCommand() { Id = id });
      return NoContent();
    }
  }
}
