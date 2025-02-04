using MediatR;
using WebApiPermissions.Domain.Entities;
using WebApiPermissions.Infrastructure.UnitOfWork;

namespace WebApiPermissions.Application.Queries
{
    public class GetPermissionsHandler : IRequestHandler<GetPermissionsQuery, IEnumerable<Permission>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPermissionsHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<IEnumerable<Permission>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Permissions.GetAllAsync();
        }
    }
}
