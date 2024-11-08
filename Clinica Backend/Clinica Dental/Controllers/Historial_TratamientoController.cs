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
    public class Historial_TratamientoController : ControllerBase
    {
        private readonly ISvGeneric<Historial_Tratamiento> _service;

        public Historial_TratamientoController(ISvGeneric<Historial_Tratamiento> service)
        {
            _service = service;
        }

        // GET: api/Historial_Tratamiento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Historial_Tratamiento>>> Get()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        // GET: api/Historial_Tratamiento/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Historial_Tratamiento>> Get(string id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST: api/Historial_Tratamiento
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Historial_Tratamiento historialTratamiento)
        {
            await _service.AddAsync(historialTratamiento);
            await _service.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = historialTratamiento.ID_Historial_Tratamiento }, historialTratamiento);
        }

        // PUT: api/Historial_Tratamiento/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] Historial_Tratamiento historialTratamiento)
        {
            if (id != historialTratamiento.ID_Historial_Tratamiento)
            {
                return BadRequest();
            }

            await _service.UpdateAsync(historialTratamiento);
            await _service.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Historial_Tratamiento/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _service.DeleteAsync(id);
            await _service.SaveChangesAsync();
            return NoContent();
        }
    }
}
