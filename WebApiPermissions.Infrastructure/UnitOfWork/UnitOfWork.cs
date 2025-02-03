using WebApiPermissions.Domain;
using WebApiPermissions.Infrastructure.Repositories;

namespace WebApiPermissions.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    public IRepository<Permission> Permissions => throw new NotImplementedException();

    public IRepository<PermissionType> PermissionTypes => throw new NotImplementedException();

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public Task SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}
