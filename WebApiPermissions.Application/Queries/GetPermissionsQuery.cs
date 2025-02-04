using MediatR;
using WebApiPermissions.Domain.Entities;

namespace WebApiPermissions.Application.Queries
{
    public record GetPermissionsQuery() : IRequest<IEnumerable<Permission>>;
}
