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
    public class Factura_TratamientoController : ControllerBase
    {
        private readonly ISvGeneric<Factura_Tratamiento> _service;

        public Factura_TratamientoController(ISvGeneric<Factura_Tratamiento> service)
        {
            _service = service;
        }

        // GET: api/Factura_Tratamiento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Factura_Tratamiento>>> Get()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        // GET: api/Factura_Tratamiento/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Factura_Tratamiento>> Get(string id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST: api/Factura_Tratamiento
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Factura_Tratamiento facturaTratamiento)
        {
            await _service.AddAsync(facturaTratamiento);
            await _service.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = facturaTratamiento.ID_Factura_Tratamiento }, facturaTratamiento);
        }

        // PUT: api/Factura_Tratamiento/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] Factura_Tratamiento facturaTratamiento)
        {
            if (id != facturaTratamiento.ID_Factura_Tratamiento)
            {
                return BadRequest();
            }

            await _service.UpdateAsync(facturaTratamiento);
            await _service.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Factura_Tratamiento/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _service.DeleteAsync(id);
            await _service.SaveChangesAsync();
            return NoContent();
        }
    }
}
