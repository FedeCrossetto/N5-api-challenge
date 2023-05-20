using Microsoft.EntityFrameworkCore;
using N5.Api.IRepository;
using N5.Api.Models;

namespace N5.Api.Repository
{
    public class PermisoRepository : IPermisoRepository
    {
        private readonly N5Context _context;

        public PermisoRepository(N5Context context)
        {
            _context = context;
        }

        public async Task<List<Permiso>> GetAll()
        {
            return await _context.Permisos.Include(p => p.TipoPermisoNavigation).ToListAsync();
        }

        public async Task<Permiso> GetById(int id)
        {
            var existingPermiso = await _context.Permisos.FindAsync(id);

            if (existingPermiso == null)
            {
                throw new ArgumentException("Permiso not found");
            }

            return await _context.Permisos.Include(p => p.TipoPermisoNavigation).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task Create(Permiso entity)
        {
            var exists = await _context.TipoPermisos.AnyAsync(p => p.Id == entity.TipoPermiso);
            if (!exists)
            {
                throw new ArgumentException("Tipo de Permiso not found");
            }

            Permiso permiso = new Permiso
            {
                Id = entity.Id,
                NombreEmpleado = entity.NombreEmpleado,
                ApellidoEmpleado = entity.ApellidoEmpleado,
                TipoPermiso = entity.TipoPermiso,
                FechaPermiso = entity.FechaPermiso
            };

            _context.Permisos.Add(permiso);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, Permiso entity)
        {
            var existingPermiso = await _context.Permisos.FindAsync(id);

            if (existingPermiso == null)
            {
                throw new ArgumentException("Permiso not found");
            }

            existingPermiso.NombreEmpleado = entity.NombreEmpleado;
            existingPermiso.ApellidoEmpleado = entity.ApellidoEmpleado;
            existingPermiso.TipoPermiso = entity.TipoPermiso;
            existingPermiso.FechaPermiso = entity.FechaPermiso;

            await _context.SaveChangesAsync();
        }
    }
}
