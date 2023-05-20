using N5.Api.Models;

namespace N5.Api.IRepository
{
    public interface IPermissionTypeRepository
    {
        Task<List<TipoPermiso>> GetAll();
        Task<TipoPermiso> GetById(int id);
        Task Create(TipoPermiso entity);
        Task Update(int id, TipoPermiso entity);
    }
}
