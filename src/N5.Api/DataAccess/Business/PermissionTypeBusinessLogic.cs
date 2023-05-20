using N5.Api.DataAccess.IBusiness;
using N5.Api.IRepository;
using N5.Api.Models;

namespace N5.Api.Business
{
    public class PermissionTypeBusinessLogic : IPermissionTypeBusinessLogic
    {
        private readonly IPermissionTypeRepository _repository;
        public PermissionTypeBusinessLogic(IPermissionTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TipoPermiso>> GetAllPermissionTypes()
        {
            return await _repository.GetAll();
        }

        public async Task<TipoPermiso> GetPermissionTypeById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task CreatePermissionType(TipoPermiso permissionType)
        {
            await _repository.Create(permissionType);
        }

        public async Task UpdatePermissionType(int id, TipoPermiso permissionType)
        {
            await _repository.Update(id, permissionType);
        }
    }
}
