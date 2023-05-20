using Microsoft.EntityFrameworkCore;
using N5.Api.IRepository;
using N5.Api.Models;
using N5.Api.Context;

namespace N5.Api.Repository
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly N5Context _context;

        public PermissionRepository(N5Context context)
        {
            _context = context;
        }

        public async Task<List<Permiso>> GetAll()
        {
            return await _context.Permisos.ToListAsync();
        }

        public async Task<Permiso> GetById(int id)
        {
            var hasPermission = await _context.Permisos.FindAsync(id);

            if (hasPermission == null)
            {
                throw new ArgumentException("Permission not found");
            }

            return await _context.Permisos.Include(p => p.TipoPermisoNavigation).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task Create(Permiso entity)
        {
            var hasPermission = await _context.TipoPermisos.AnyAsync(p => p.Id == entity.TipoPermiso);
            if (!hasPermission)
            {
                throw new ArgumentException("Permission Type not found");
            }

            Permiso permission = new Permiso
            {
                Id = entity.Id,
                NombreEmpleado = entity.NombreEmpleado,
                ApellidoEmpleado = entity.ApellidoEmpleado,
                TipoPermiso = entity.TipoPermiso,
                FechaPermiso = entity.FechaPermiso
            };

            _context.Permisos.Add(permission);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, Permiso entity)
        {
            var permission = await _context.Permisos.FindAsync(id);

            if (permission == null)
            {
                throw new ArgumentException("Permission not found");
            }

            permission.NombreEmpleado = entity.NombreEmpleado;
            permission.ApellidoEmpleado = entity.ApellidoEmpleado;
            permission.TipoPermiso = entity.TipoPermiso;
            permission.FechaPermiso = entity.FechaPermiso;

            await _context.SaveChangesAsync();
        }
    }
}
