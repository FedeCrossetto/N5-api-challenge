using N5.Api.Models;

namespace N5.Api.DataAccess.IBusiness
{
    public interface IPermissionTypeBusinessLogic
    {
        Task<List<TipoPermiso>> GetAllPermissionTypes();
        Task<TipoPermiso> GetPermissionTypeById(int id);
        Task CreatePermissionType(TipoPermiso permissionType);
        Task UpdatePermissionType(int id, TipoPermiso permissionType);

    }
}
