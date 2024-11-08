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
    public class ProcedimientoController : ControllerBase
    {
        private readonly ISvGeneric<Procedimiento> _service;

        public ProcedimientoController(ISvGeneric<Procedimiento> service)
        {
            _service = service;
        }

        // GET: api/Procedimiento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Procedimiento>>> Get()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        // GET: api/Procedimiento/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Procedimiento>> Get(string id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST: api/Procedimiento
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Procedimiento procedimiento)
        {
            await _service.AddAsync(procedimiento);
            await _service.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = procedimiento.ID_Procedimiento }, procedimiento);
        }

        // PUT: api/Procedimiento/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] Procedimiento procedimiento)
        {
            if (id != procedimiento.ID_Procedimiento)
            {
                return BadRequest();
            }

            await _service.UpdateAsync(procedimiento);
            await _service.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Procedimiento/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _service.DeleteAsync(id);
            await _service.SaveChangesAsync();
            return NoContent();
        }
    }
}
