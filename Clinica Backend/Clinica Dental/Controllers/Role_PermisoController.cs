using Application.Dtos.PostDtos;
using Application.Services;
using Domain.Interfaces.JD_Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesPermisosController : ControllerBase
    {
        private readonly ISvRolesPermiso _svRolesPermiso;

        public RolesPermisosController(ISvRolesPermiso svRolesPermiso)
        {
            _svRolesPermiso = svRolesPermiso;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RolesPermisosPostDto>>> GetAllRolesPermisos()
        {
            var rolesPermisos = await _svRolesPermiso.GetAllRolesPermisosAsync();
            return Ok(rolesPermisos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RolesPermisosPostDto>> GetRolesPermisoById(string id)
        {
            try
            {
                var rolesPermiso = await _svRolesPermiso.GetRolesPermisoByIdAsync(id);
                return Ok(rolesPermiso);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> RegisterRolesPermiso([FromBody] RolesPermisosPostDto rolesPermisosDto)
        {
            try
            {
                await _svRolesPermiso.RegisterRolesPermisoAsync(rolesPermisosDto);
                return CreatedAtAction(nameof(GetRolesPermisoById), new { id = rolesPermisosDto.ID_Roles_Permisos }, rolesPermisosDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRolesPermiso(string id, [FromBody] RolesPermisosPostDto rolesPermisosDto)
        {
            if (id != rolesPermisosDto.ID_Roles_Permisos)
            {
                return BadRequest("ID del RolesPermiso no coincide");
            }

            try
            {
                await _svRolesPermiso.UpdateRolesPermisoAsync(rolesPermisosDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRolesPermiso(string id)
        {
            try
            {
                await _svRolesPermiso.DeleteRolesPermisoAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
