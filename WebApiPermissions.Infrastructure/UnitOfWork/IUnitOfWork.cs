using WebApiPermissions.Domain; // Asegúrate de que las entidades están en este namespace
using WebApiPermissions.Infrastructure.Repositories;

namespace WebApiPermissions.Infrastructure.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IRepository<Permission> Permissions { get; }
    IRepository<PermissionType> PermissionTypes { get; }
    Task SaveChangesAsync();
}
