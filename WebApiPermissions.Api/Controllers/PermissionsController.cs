using Elastic.Clients.Elasticsearch;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApiPermissions.Application.Commands;
using WebApiPermissions.Application.Queries;
using WebApiPermissions.Domain.Entities;
using WebApiPermissions.Infrastructure.Services;

[ApiController]
[Route("api/permissions")]
public class PermissionsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ElasticsearchService _elasticsearchService;
    private readonly ElasticsearchClient _elasticClient;

    public PermissionsController(
         ElasticsearchService elasticsearchService,
         ElasticsearchClient elasticClient,
        IMediator mediator)
    {
        _elasticsearchService = elasticsearchService;
        _mediator = mediator;
        _elasticClient = elasticClient;
    }

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

    [HttpGet("search")]
    public async Task<IActionResult> SearchPermissions([FromQuery] string query)
    {
        var response = await _elasticClient.SearchAsync<Permission>(s => s
            .Index("permissions")
            .Query(q => q
                .Match(m => m
                    .Field(f => f.NombreEmpleado)
                    .Query(query)
                )
            )
        );

        if (!response.IsValidResponse)
        {
            return BadRequest($"Error en la búsqueda: {response.DebugInformation}");
        }

        return Ok(response.Documents);
    }
}
