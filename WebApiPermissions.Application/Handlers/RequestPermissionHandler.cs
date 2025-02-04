using MediatR;
using WebApiPermissions.Application.Commands;
using WebApiPermissions.Domain.Entities;
using WebApiPermissions.Infrastructure.UnitOfWork;

namespace WebApiPermissions.Application.Handlers;

public class RequestPermissionHandler : IRequestHandler<RequestPermissionCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public RequestPermissionHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(RequestPermissionCommand request, CancellationToken cancellationToken)
    {
        var permission = new Permission
        {
            NombreEmpleado = request.NombreEmpleado,
            ApellidoEmpleado = request.ApellidoEmpleado,
            TipoPermisoId = request.TipoPermisoId,
            FechaPermiso = request.FechaPermiso
            
        };

        await _unitOfWork.Permissions.AddAsync(permission);
        await _unitOfWork.SaveChangesAsync();

        return permission.Id; 
    }
}
