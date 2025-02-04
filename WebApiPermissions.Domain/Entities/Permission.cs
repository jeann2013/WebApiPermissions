namespace WebApiPermissions.Domain.Entities;

public class Permission
{
    public int Id { get; set; }
    public required string NombreEmpleado { get; set; }
    public required string ApellidoEmpleado { get; set; }
    public required int TipoPermisoId { get; set; }
    public required DateTime FechaPermiso { get; set; }

    public virtual PermissionType TipoPermiso { get; set; }
}


