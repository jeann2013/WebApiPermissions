using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApiPermissions.Domain.Commands;

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
}
