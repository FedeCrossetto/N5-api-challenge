using N5.Api.Models;

namespace N5.Api.DataAccess.IBusiness
{
    public interface IPermissionBusinessLogic
    {
        Task<List<Permiso>> GetAllPermissions();
        Task<Permiso> GetPermissionById(int id);
        Task CreatePermission(Permiso permissionData);
        Task UpdatePermission(int id, Permiso permission);
    }
}
