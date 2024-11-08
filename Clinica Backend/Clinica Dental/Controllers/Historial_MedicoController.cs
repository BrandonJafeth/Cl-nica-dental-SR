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
    public class Historial_MedicoController : ControllerBase
    {
        private readonly ISvGeneric<Historial_Medico> _service;

        public Historial_MedicoController(ISvGeneric<Historial_Medico> service)
        {
            _service = service;
        }

        // GET: api/Historial_Medico
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Historial_Medico>>> Get()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        // GET: api/Historial_Medico/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Historial_Medico>> Get(string id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST: api/Historial_Medico
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Historial_Medico historialMedico)
        {
            await _service.AddAsync(historialMedico);
            await _service.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = historialMedico.ID_HistorialMedico }, historialMedico);
        }

        // PUT: api/Historial_Medico/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] Historial_Medico historialMedico)
        {
            if (id != historialMedico.ID_HistorialMedico)
            {
                return BadRequest();
            }

            await _service.UpdateAsync(historialMedico);
            await _service.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Historial_Medico/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _service.DeleteAsync(id);
            await _service.SaveChangesAsync();
            return NoContent();
        }
    }
}
