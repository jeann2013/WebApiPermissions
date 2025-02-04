using FluentAssertions;
using Moq;
using WebApiPermissions.Application.Commands;
using WebApiPermissions.Application.Handlers;
using WebApiPermissions.Domain.Entities;
using WebApiPermissions.Infrastructure.Repositories;
using WebApiPermissions.Infrastructure.UnitOfWork;

public class RequestPermissionHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnPermissionId_WhenSuccessful()
    {

        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var permissionRepoMock = new Mock<IRepository<Permission>>();

        unitOfWorkMock.Setup(u => u.Permissions).Returns(permissionRepoMock.Object);
        unitOfWorkMock.Setup(u => u.SaveChangesAsync()).Returns(Task.CompletedTask);


        permissionRepoMock.Setup(r => r.AddAsync(It.IsAny<Permission>()))
                          .Callback<Permission>(p => p.Id = 1);

        var handler = new RequestPermissionHandler(unitOfWorkMock.Object);

        var command = new RequestPermissionCommand(
            1, "Juan", "Pérez", 1, DateTime.Now
        );

        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().BeGreaterThan(0);
        permissionRepoMock.Verify(r => r.AddAsync(It.IsAny<Permission>()), Times.Once);
        unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
    }
}
