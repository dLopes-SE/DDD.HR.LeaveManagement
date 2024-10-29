using Application.Features.LeaveType.Command.CreateLeaveType;
using Application.Features.LeaveType.Command.DeleteLeaveType;
using Application.Features.LeaveType.Command.UpdateLeaveType;
using Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class LeaveTypesController(IMediator mediator) : Controller
  {
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    public async Task<ActionResult<List<LeaveTypeDto>>> Get()
    {
      var leaveTypes = await _mediator.Send(new GetLeaveTypesQuery());
      if (leaveTypes.Count is 0)
        return NoContent();

      return Ok(leaveTypes);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<LeaveTypeDto>> Get(int id)
    {
      var leaveType = await _mediator.Send(new GetLeaveTypeDetailsQuery(id));
      return Ok(leaveType);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<int>> Create([FromBody] CreateLeaveTypeCommand leaveTypeObj)
    {
      var id = await _mediator.Send(leaveTypeObj);
      //return CreatedAtAction(nameof(Create), new { id }); => This produces a JSON response like '{id: 1}'. => however, the client expects a plain text response with the id (due to the use of nswag)
      return new ContentResult
      {
        StatusCode = 201,
        Content = id.ToString(),
        ContentType = "text/plain"
      };
    }

    [HttpPut]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> Update([FromBody] UpdateLeaveTypeCommand leaveTypeObj)
    {
      await _mediator.Send(leaveTypeObj);
      return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> Delete(int id)
    {
      await _mediator.Send(new DeleteLeaveTypeCommand()
      {
        Id = id
      });
      return NoContent();
    }
  }
}