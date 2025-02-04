using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApiPermissions.Application.Commands;
using WebApiPermissions.Application.Queries;
using WebApiPermissions.Domain.Commands;
using WebApiPermissions.Domain.Entities;

[ApiController]
[Route("api/permissions")]
public class PermissionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PermissionsController(IMediator mediator) => _mediator = mediator;


    [HttpPost]
    public async Task<IActionResult> RequestPermission([FromBody] RequestPermissionCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(RequestPermission), new { id = result }, result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Permission>>> GetPermissions()
    {
        var result = await _mediator.Send(new GetPermissionsQuery());
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ModifyPermission(int id, [FromBody] ModifyPermissionCommand command)
    {
        if (id != command.Id)
            return BadRequest("El ID en la URL y en el cuerpo deben coincidir.");

        var result = await _mediator.Send(command);
        return result ? NoContent() : NotFound();
    }
}
