using WebApiPermissions.Domain.Entities;
using WebApiPermissions.Infrastructure.Data;
using WebApiPermissions.Infrastructure.Repositories;

namespace WebApiPermissions.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private Repository<Permission> _permissions;
        private Repository<PermissionType> _permissionTypes;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IRepository<Permission> Permissions => _permissions ??= new Repository<Permission>(_context);
        public IRepository<PermissionType> PermissionTypes => _permissionTypes ??= new Repository<PermissionType>(_context);

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();
    }
}
