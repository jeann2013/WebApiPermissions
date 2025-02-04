using Microsoft.EntityFrameworkCore;
using WebApiPermissions.Domain.Entities;

namespace WebApiPermissions.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Permission> Permissions { get; set; }
    public DbSet<PermissionType> PermissionTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Relacion entre Permission y PermissionType
        modelBuilder.Entity<Permission>()
            .HasOne(p => p.TipoPermiso)
            .WithMany()
            .HasForeignKey(p => p.TipoPermisoId);
    }
}
