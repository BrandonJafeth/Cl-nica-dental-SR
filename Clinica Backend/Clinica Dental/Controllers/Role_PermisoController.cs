using Application.GenericService;
using Clinica_Dental;
using Domain.Interfaces.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinica_Dental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Role_PermisoController : ControllerBase
    {
        private readonly ISvGeneric<Roles_Permiso> _service;

        public Role_PermisoController(ISvGeneric<Roles_Permiso> service)
        {
            _service = service;
        }

        // GET: api/Role_Permiso
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Roles_Permiso>>> Get()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        // GET: api/Role_Permiso/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Roles_Permiso>> Get(string id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST: api/Role_Permiso
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Roles_Permiso rolesPermiso)
        {
            await _service.AddAsync(rolesPermiso);
            await _service.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = rolesPermiso.ID_Roles_Permisos }, rolesPermiso);
        }

        // PUT: api/Role_Permiso/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] Roles_Permiso rolesPermiso)
        {
            if (id != rolesPermiso.ID_Roles_Permisos)
            {
                return BadRequest();
            }

            await _service.UpdateAsync(rolesPermiso);
            await _service.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Role_Permiso/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _service.DeleteAsync(id);
            await _service.SaveChangesAsync();
            return NoContent();
        }
    }
}

