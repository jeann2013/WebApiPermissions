using FluentAssertions;
using Moq;
using WebApiPermissions.Application.Commands;
using WebApiPermissions.Application.Handlers;
using WebApiPermissions.Domain.Entities;
using WebApiPermissions.Infrastructure.Repositories;
using WebApiPermissions.Infrastructure.UnitOfWork;

public class ModifyPermissionHandlerTests
{
    [Fact]
    public async Task Handle_ShouldModifyExistingPermission_WhenSuccessful()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var permissionRepoMock = new Mock<IRepository<Permission>>();

        var existingPermission = new Permission
        {
            Id = 1,
            NombreEmpleado = "Juan",
            ApellidoEmpleado = "Pérez",
            TipoPermisoId = 1,
            FechaPermiso = System.DateTime.Now
        };

        // Simular que GetByIdAsync devuelve un permiso existente
        permissionRepoMock.Setup(r => r.GetByIdAsync(1))
                          .ReturnsAsync(existingPermission);

        unitOfWorkMock.Setup(u => u.Permissions).Returns(permissionRepoMock.Object);
        unitOfWorkMock.Setup(u => u.SaveChangesAsync()).Returns(Task.CompletedTask);

        var handler = new ModifyPermissionHandler(unitOfWorkMock.Object);

        var command = new ModifyPermissionCommand(
            1, // ID del permiso a modificar
            "Carlos", "Gómez", 2, DateTime.Now
        );

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeTrue();
        existingPermission.NombreEmpleado.Should().Be("Carlos");
        existingPermission.ApellidoEmpleado.Should().Be("Gómez");
        permissionRepoMock.Verify(r => r.Update(existingPermission), Times.Once);
        unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
    }
}
