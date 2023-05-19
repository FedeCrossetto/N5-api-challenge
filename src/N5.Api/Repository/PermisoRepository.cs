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
            return await _context.Permisos
                .Include(p => p.TipoPermisoNavigation)
                .ToListAsync();
        }

        public async Task<Permiso> GetById(int id)
        {
            return await _context.Permisos
                .Include(p => p.TipoPermisoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Add(Permiso permiso)
        {
            _context.Permisos.Add(permiso);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Permiso permiso)
        {
            _context.Update(permiso);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var permiso = await _context.Permisos.FindAsync(id);
            if (permiso != null)
            {
                _context.Permisos.Remove(permiso);
                await _context.SaveChangesAsync();
            }
        }

        public bool Exists(int id)
        {
            return _context.Permisos.Any(e => e.Id == id);
        }
    }
}
