using MediatR;

namespace WebApiPermissions.Application.Commands;
public record RequestPermissionCommand(int Id, string NombreEmpleado,
    string ApellidoEmpleado, int TipoPermisoId, DateTime FechaPermiso) : IRequest<int>;
