using Application.Features.LeaveAllocation.Command.CreateLeaveAllocation;
using Application.Features.LeaveAllocation.Command.DeleteLeaveAllocation;
using Application.Features.LeaveAllocation.Command.UpdateLeaveAllocation;
using Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations;
using Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class LeaveAllocationController(IMediator mediator) : Controller
  {
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    public async Task<ActionResult<List<LeaveAllocationDto>>> Get()
    {
      var leaveAllocations = await mediator.Send(new GetLeaveAllocationsQuery());
      if (leaveAllocations.Count is 0)
        return NoContent();

      return Ok(leaveAllocations);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<LeaveAllocationDetailsDto>> Get(int id)
    {
      var leaveAllocation = await mediator.Send(new GetLeaveAllocationDetailsQuery()
      {
        Id = id
      });
      return Ok(leaveAllocation);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<int>> Create(CreateLeaveAllocationCommand leaveAllocationObj)
    {
      var id = await mediator.Send(leaveAllocationObj);
      return CreatedAtAction(nameof(Get), new { id });
    }

    [HttpPut]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> Update([FromBody] UpdateLeaveAllocationCommand leaveAllocationObj)
    {
      await mediator.Send(leaveAllocationObj);
      return NoContent();
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> Delete(int id)
    {
      await mediator.Send(new DeleteLeaveAllocationCommand()
      {
        Id = id
      });
      return NoContent();
    }
  }
}
