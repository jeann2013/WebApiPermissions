namespace WebApiPermissions.Domain.Commands;

public class RequestPermissionCommand
{
    public required string NombreEmpleado { get; set; }
    public required string ApellidoEmpleado { get; set; }
    public required int TipoPermisoId { get; set; }
    public required DateTime FechaPermiso { get; set; }
}
