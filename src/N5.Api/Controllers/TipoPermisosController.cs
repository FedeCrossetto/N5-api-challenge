using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N5.Api.Models;

namespace N5.Api.Controllers
{
    public class TipoPermisosController : Controller
    {
        private readonly N5Context _context;

        public TipoPermisosController(N5Context context)
        {
            _context = context;
        }

        // GET: TipoPermisos
        public async Task<IActionResult> Index()
        {
              return _context.TipoPermisos != null ? 
                          View(await _context.TipoPermisos.ToListAsync()) :
                          Problem("Entity set 'N5Context.TipoPermisos'  is null.");
        }

        // GET: TipoPermisos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TipoPermisos == null)
            {
                return NotFound();
            }

            var tipoPermiso = await _context.TipoPermisos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoPermiso == null)
            {
                return NotFound();
            }

            return View(tipoPermiso);
        }

        // GET: TipoPermisos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoPermisos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion")] TipoPermiso tipoPermiso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoPermiso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoPermiso);
        }

        // GET: TipoPermisos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TipoPermisos == null)
            {
                return NotFound();
            }

            var tipoPermiso = await _context.TipoPermisos.FindAsync(id);
            if (tipoPermiso == null)
            {
                return NotFound();
            }
            return View(tipoPermiso);
        }

        // POST: TipoPermisos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion")] TipoPermiso tipoPermiso)
        {
            if (id != tipoPermiso.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoPermiso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoPermisoExists(tipoPermiso.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tipoPermiso);
        }

        // GET: TipoPermisos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TipoPermisos == null)
            {
                return NotFound();
            }

            var tipoPermiso = await _context.TipoPermisos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoPermiso == null)
            {
                return NotFound();
            }

            return View(tipoPermiso);
        }

        // POST: TipoPermisos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TipoPermisos == null)
            {
                return Problem("Entity set 'N5Context.TipoPermisos'  is null.");
            }
            var tipoPermiso = await _context.TipoPermisos.FindAsync(id);
            if (tipoPermiso != null)
            {
                _context.TipoPermisos.Remove(tipoPermiso);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoPermisoExists(int id)
        {
          return (_context.TipoPermisos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
