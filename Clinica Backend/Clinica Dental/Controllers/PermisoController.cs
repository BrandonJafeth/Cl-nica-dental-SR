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
    public class PermisoController : ControllerBase
    {
        private readonly ISvGeneric<Permiso> _service;

        public PermisoController(ISvGeneric<Permiso> service)
        {
            _service = service;
        }

        // GET: api/Permiso
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Permiso>>> Get()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        // GET: api/Permiso/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Permiso>> Get(string id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST: api/Permiso
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Permiso permiso)
        {
            await _service.AddAsync(permiso);
            await _service.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = permiso.ID_Permisos }, permiso);
        }

        // PUT: api/Permiso/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] Permiso permiso)
        {
            if (id != permiso.ID_Permisos)
            {
                return BadRequest();
            }

            await _service.UpdateAsync(permiso);
            await _service.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Permiso/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _service.DeleteAsync(id);
            await _service.SaveChangesAsync();
            return NoContent();
        }
    }
}
