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
    public class PacienteController : ControllerBase
    {
        private readonly ISvGeneric<Paciente> _service;

        public PacienteController(ISvGeneric<Paciente> service)
        {
            _service = service;
        }

        // GET: api/Paciente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Paciente>>> Get()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        // GET: api/Paciente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Paciente>> Get(string id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST: api/Paciente
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Paciente paciente)
        {
            await _service.AddAsync(paciente);
            await _service.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = paciente.ID_Paciente }, paciente);
        }

        // PUT: api/Paciente/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] Paciente paciente)
        {
            if (id != paciente.ID_Paciente)
            {
                return BadRequest();
            }

            await _service.UpdateAsync(paciente);
            await _service.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Paciente/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _service.DeleteAsync(id);
            await _service.SaveChangesAsync();
            return NoContent();
        }
    }
}
