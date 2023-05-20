using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N5.Api.DataAccess;
using N5.Api.DataAccess.IBusiness;
using N5.Api.IRepository;
using N5.Api.Models;

namespace N5.Api.Controllers
{
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionBusinessLogic _permissionBusinessLogic;
        public PermissionController(IPermissionBusinessLogic permissionBusinessLogic)
        {
            _permissionBusinessLogic = permissionBusinessLogic;
        }

        [HttpGet("api/[controller]")]
        public async Task<ActionResult<List<Permiso>>> GetAllPermissions()
        {
            var permisos = await _permissionBusinessLogic.GetAllPermissions();
            return Ok(permisos);
        }

        [HttpGet("api/[controller]/{id}")]
        public async Task<ActionResult<Permiso>> GetPermissionById(int id)
        {
            try
            {
                var permiso = await _permissionBusinessLogic.GetPermissionById(id);
                return Ok(permiso);
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string> { Success = false, ErrorMessage = ex.Message });
            }
        }

        [HttpPost("permisos")]
        public async Task<IActionResult> CreatePermission([FromBody] Permiso permisoData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<string> { Success = false, ErrorMessage = "Invalid data" });
            }
            try
            {
                await _permissionBusinessLogic.CreatePermission(permisoData);
                return Ok(new ApiResponse<object> { Success = true });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ApiResponse<string> { Success = false, ErrorMessage = ex.Message });
            }
        }

        [HttpPut("api/[controller]/{id}")]
        public async Task<IActionResult> UpdatePermission([FromRoute] int id, [FromBody] Permiso permiso)
        {
            try
            {
                await _permissionBusinessLogic.UpdatePermission(id, permiso);
                return Ok(new ApiResponse<string> { Success = true });
            }
            catch (ArgumentException ex)
            {
                return NotFound(new ApiResponse<string> { Success = false, ErrorMessage = ex.Message });
            }
        }

    }
}
