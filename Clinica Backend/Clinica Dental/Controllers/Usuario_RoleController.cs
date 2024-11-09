using Application.Dtos.PostDtos;
using Domain.Interfaces.JD_Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioRolesController : ControllerBase
    {
        private readonly ISvUsuarioRoles _svUsuarioRoles;

        public UsuarioRolesController(ISvUsuarioRoles svUsuarioRoles)
        {
            _svUsuarioRoles = svUsuarioRoles;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioRolesDto>>> GetAllUsuarioRoles()
        {
            var usuarioRoles = await _svUsuarioRoles.GetAllUsuarioRolesAsync();
            return Ok(usuarioRoles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioRolesDto>> GetUsuarioRolesById(string id)
        {
            var usuarioRole = await _svUsuarioRoles.GetUsuarioRolesByIdAsync(id);

            if (usuarioRole == null)
            {
                return NotFound();
            }

            return Ok(usuarioRole);
        }

        [HttpPost]
        public async Task<ActionResult> RegisterUsuarioRoles([FromBody] UsuarioRolesDto usuarioRolesDto)
        {
            await _svUsuarioRoles.RegisterUsuarioRolesAsync(usuarioRolesDto);
            return CreatedAtAction(nameof(GetUsuarioRolesById), new { id = usuarioRolesDto.ID_Usuario_Roles }, usuarioRolesDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUsuarioRoles(string id, [FromBody] UsuarioRolesDto usuarioRolesDto)
        {
            if (id != usuarioRolesDto.ID_Usuario_Roles)
            {
                return BadRequest();
            }

            await _svUsuarioRoles.UpdateUsuarioRolesAsync(usuarioRolesDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUsuarioRoles(string id)
        {
            await _svUsuarioRoles.DeleteUsuarioRolesAsync(id);
            return NoContent();
        }
    }
}


