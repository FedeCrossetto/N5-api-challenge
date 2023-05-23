using Microsoft.EntityFrameworkCore;
using N5.Api.IRepository;
using N5.Api.Models;
using N5.Api.Context;

namespace N5.Api.Repository
{
    public class PermissionTypeRepository : IPermissionTypeRepository
    {
        private readonly N5Context _context;

        public PermissionTypeRepository(N5Context context)
        {
            _context = context;
        }

        public async Task<List<TipoPermiso>> GetAll()
        {
            return await _context.TipoPermisos.ToListAsync();
        }

        public async Task<TipoPermiso> GetById(int id)
        {
            var permissionType = await _context.TipoPermisos.FindAsync(id);

            if (permissionType == null)
            {
                throw new ArgumentException("Permission Type not found");
            }

            return permissionType;
        }

        public async Task Create(TipoPermiso entity)
        {
            TipoPermiso permissionType = new TipoPermiso
            {
                Id = entity.Id,
                Descripcion = entity.Descripcion
            };

            _context.TipoPermisos.Add(permissionType);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, TipoPermiso entity)
        {
            var permission = await _context.TipoPermisos.FindAsync(id);

            if (permission == null)
            {
                throw new ArgumentException("Permission Type not found");
            }

            permission.Id = entity.Id;
            permission.Descripcion = entity.Descripcion;

            await _context.SaveChangesAsync();
        }
    }
}
