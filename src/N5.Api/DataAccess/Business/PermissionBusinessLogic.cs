using N5.Api.DataAccess.IBusiness;
using N5.Api.IRepository;
using N5.Api.Models;

namespace N5.Api.Business
{
    public class PermissionBusinessLogic : IPermissionBusinessLogic
    {
        private readonly IPermissionRepository _repository;

        public PermissionBusinessLogic(IPermissionRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Permiso>> GetAllPermissions()
        {
            return await _repository.GetAll();
        }

        public async Task<Permiso> GetPermissionById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task CreatePermission(Permiso permissionData)
        {
            await _repository.Create(permissionData);
        }

        public async Task UpdatePermission(int id, Permiso permission)
        {
            await _repository.Update(id, permission);
        }
    }
}
