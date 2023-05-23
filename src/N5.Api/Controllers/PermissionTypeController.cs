using Microsoft.AspNetCore.Mvc;
using N5.Api.IRepository;
using N5.Api.Models;
using N5.Api.DataAccess.IBusiness;

namespace N5.Api.Controllers
{
    public class PermissionTypeController : ControllerBase
    {
        private readonly IPermissionTypeBusinessLogic _permissionTypeBusinessLogic;
        public PermissionTypeController(IPermissionTypeRepository tipoPermisoRepository, IPermissionTypeBusinessLogic permissionTypeBusinessLogic)
        {
            _permissionTypeBusinessLogic = permissionTypeBusinessLogic;
        }

        [HttpGet("api/[controller]")]
        public async Task<ActionResult<List<TipoPermiso>>> GetAllPermissions()
        {
            var permisos = await _permissionTypeBusinessLogic.GetAllPermissionTypes();
            return Ok(permisos);
        }

        [HttpGet("api/[controller]/{id}")]
        public async Task<ActionResult<Permiso>> GetPermissionTypeById(int id)
        {
            try
            {
                var permiso = await _permissionTypeBusinessLogic.GetPermissionTypeById(id);
                return Ok(permiso);
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string> { Success = false, ErrorMessage = ex.Message });
            }
        }

        [HttpPost("api/[controller]")]
        public async Task<IActionResult> CreatePermissionType([FromBody] TipoPermiso permissionType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<string> { Success = false, ErrorMessage = "Invalid data" });
            }
            try
            {
                await _permissionTypeBusinessLogic.CreatePermissionType(permissionType);
                return Ok(new ApiResponse<object> { Success = true });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ApiResponse<string> { Success = false, ErrorMessage = ex.Message });
            }
        }

        [HttpPut("api/[controller]/{id}")]
        public async Task<IActionResult> UpdatePermissionType([FromRoute] int id, [FromBody] TipoPermiso permissionType)
        {
            try
            {
                await _permissionTypeBusinessLogic.UpdatePermissionType(id, permissionType);
                return Ok(new ApiResponse<string> { Success = true });
            }
            catch (ArgumentException ex)
            {
                return NotFound(new ApiResponse<string> { Success = false, ErrorMessage = ex.Message });
            }
        }


    }
}
