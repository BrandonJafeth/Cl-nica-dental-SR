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
    public class RolesController : ControllerBase
    {
        private readonly ISvRole _svRole;

        public RolesController(ISvRole svRole)
        {
            _svRole = svRole;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RolesPostDto>> GetRoleById(string id)
        {
            var role = await _svRole.GetRoleByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RolesPostDto>>> GetAllRoles()
        {
            var roles = await _svRole.GetAllRolesAsync();
            return Ok(roles);
        }

        [HttpPost]
        public async Task<ActionResult> RegisterRole(RolesPostDto roleDto)
        {
            await _svRole.RegisterRoleAsync(roleDto);
            return CreatedAtAction(nameof(GetRoleById), new { id = roleDto.ID_Roles }, roleDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRole(string id, RolesPostDto roleDto)
        {
            if (id != roleDto.ID_Roles)
            {
                return BadRequest();
            }

            await _svRole.UpdateRoleAsync(roleDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRole(string id)
        {
            await _svRole.DeleteRoleAsync(id);
            return NoContent();
        }
    }
}



