using Microsoft.EntityFrameworkCore;
using WebApiPermissions.Domain;

namespace WebApiPermissions.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Permission> Permissions { get; set; }
    public DbSet<PermissionType> PermissionTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Permission>()
            .HasOne(p => p.TipoPermiso)
            .WithMany()
            .HasForeignKey(p => p.TipoPermisoId);
    }
}
