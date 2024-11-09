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
    public class PermisosController : ControllerBase
    {
        private readonly ISvPermiso _svPermiso;

        public PermisosController(ISvPermiso svPermiso)
        {
            _svPermiso = svPermiso;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PermisosPostDto>>> GetAllPermisos()
        {
            var permisos = await _svPermiso.GetAllPermisosAsync();
            return Ok(permisos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PermisosPostDto>> GetPermisoById(string id)
        {
            try
            {
                var permiso = await _svPermiso.GetPermisoByIdAsync(id);
                return Ok(permiso);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> RegisterPermiso([FromBody] PermisosPostDto permisoDto)
        {
            try
            {
                await _svPermiso.RegisterPermisoAsync(permisoDto);
                return CreatedAtAction(nameof(GetPermisoById), new { id = permisoDto.ID_Permisos }, permisoDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePermiso(string id, [FromBody] PermisosPostDto permisoDto)
        {
            if (id != permisoDto.ID_Permisos)
            {
                return BadRequest("ID del permiso no coincide");
            }

            try
            {
                await _svPermiso.UpdatePermisoAsync(permisoDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePermiso(string id)
        {
            try
            {
                await _svPermiso.DeletePermisoAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
