
using N5.Api.Models;

namespace N5.Api.IRepository
{
    public interface IPermisoRepository
    {
        Task<List<Permiso>> GetAll();
        Task<Permiso> GetById(int id);
        Task Add(Permiso permiso);
        Task Update(Permiso permiso);
        Task Delete(int id);
        bool Exists(int id);
    }
}
