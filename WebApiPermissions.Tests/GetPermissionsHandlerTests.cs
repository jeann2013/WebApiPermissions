using FluentAssertions;
using Moq;
using WebApiPermissions.Application.Queries;
using WebApiPermissions.Domain.Entities;
using WebApiPermissions.Infrastructure.Repositories;
using WebApiPermissions.Infrastructure.UnitOfWork;

public class GetPermissionsHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnListOfPermissions()
    {
        
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var permissionRepoMock = new Mock<IRepository<Permission>>();

        var permissions = new List<Permission>
        {
            new Permission { Id = 1, NombreEmpleado = "Juan", ApellidoEmpleado = "Pérez", TipoPermisoId = 1, FechaPermiso = System.DateTime.Now },
            new Permission { Id = 2, NombreEmpleado = "María", ApellidoEmpleado = "López", TipoPermisoId = 2, FechaPermiso = System.DateTime.Now }
        };
        
        permissionRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(permissions);
        unitOfWorkMock.Setup(u => u.Permissions).Returns(permissionRepoMock.Object);

        var handler = new GetPermissionsHandler(unitOfWorkMock.Object);
        var query = new GetPermissionsQuery();
        
        var result = await handler.Handle(query, CancellationToken.None);
        
        result.Should().HaveCount(2);
        result.Should().Contain(p => p.NombreEmpleado == "Juan");
        result.Should().Contain(p => p.NombreEmpleado == "María");
        permissionRepoMock.Verify(r => r.GetAllAsync(), Times.Once);
    }
}
