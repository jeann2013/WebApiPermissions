using MediatR;
using WebApiPermissions.Application.Commands;
using WebApiPermissions.Infrastructure.UnitOfWork;

namespace WebApiPermissions.Application.Handlers;

public class ModifyPermissionHandler : IRequestHandler<ModifyPermissionCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public ModifyPermissionHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<bool> Handle(ModifyPermissionCommand request, CancellationToken cancellationToken)
    {
        var permission = await _unitOfWork.Permissions.GetByIdAsync(request.Id);
        if (permission == null) return false;

        permission.NombreEmpleado = request.NombreEmpleado;
        permission.ApellidoEmpleado = request.ApellidoEmpleado;
        permission.TipoPermisoId = request.TipoPermisoId;
        permission.FechaPermiso = request.FechaPermiso;

        _unitOfWork.Permissions.Update(permission);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
