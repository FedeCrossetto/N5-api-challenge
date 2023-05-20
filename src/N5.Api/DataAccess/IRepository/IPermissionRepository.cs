using N5.Api.Models;

namespace N5.Api.IRepository
{
    public interface IPermissionRepository
    {
        Task<List<Permiso>> GetAll();
        Task<Permiso> GetById(int id);
        Task Create(Permiso entity);
        Task Update(int id, Permiso entity);
    }
}
