using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N5.Api.IRepository;
using N5.Api.Models;

namespace N5.Api.Controllers
{
    public class PermisosController : Controller
    {
        private readonly IPermisoRepository _repository;
        public PermisosController(N5Context context, IPermisoRepository permisoRepository)
        {
            _repository = permisoRepository;
        }


        [HttpGet("api/[controller]")]
        public async Task<ActionResult<List<Permiso>>> GetAllPermisos()
        {
            var permisos = await _repository.GetAll();
            return Ok(permisos);
        }


        [HttpGet("api/[controller]/{id}")]
        public async Task<ActionResult<Permiso>> GetPermisoById(int id)
        {
            try
            {
                var permiso = await _repository.GetById(id);
                return Ok(permiso);
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string> { Success = false, ErrorMessage = ex.Message });
            }
        }

        // POST: api/permiso
        [HttpPost("permisos")]
        public async Task<IActionResult> CreatePermiso([FromBody] Permiso permisoData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<string> { Success = false, ErrorMessage = "Invalid data" });
            }
            try
            {
                await _repository.Create(permisoData);
                return Ok(new ApiResponse<object> { Success = true });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ApiResponse<string> { Success = false, ErrorMessage = ex.Message });
            }
        }

        // PUT: api/permiso/{id}
        [HttpPut("api/[controller]/{id}")]
        public async Task<IActionResult> UpdatePermiso([FromRoute] int id, [FromBody] Permiso permiso)
        {
            try
            {
                await _repository.Update(id, permiso);
                return Ok();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

    }
}
