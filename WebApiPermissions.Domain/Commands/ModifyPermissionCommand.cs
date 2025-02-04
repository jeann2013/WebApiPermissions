using MediatR;

namespace WebApiPermissions.Application.Commands;

public record ModifyPermissionCommand(int Id, string NombreEmpleado, 
    string ApellidoEmpleado, int TipoPermisoId, DateTime FechaPermiso) : IRequest<bool>;
