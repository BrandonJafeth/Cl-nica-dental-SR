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
    public class PagoController : ControllerBase
    {
        private readonly ISvGeneric<Pago> _service;

        public PagoController(ISvGeneric<Pago> service)
        {
            _service = service;
        }

        // GET: api/Pago
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pago>>> Get()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        // GET: api/Pago/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pago>> Get(String id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST: api/Pago
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Pago pago)
        {
            await _service.AddAsync(pago);
            await _service.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = pago.ID_Pago }, pago);
        }

        // PUT: api/Pago/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] Pago pago)
        {
            if (id != pago.ID_Pago)
            {
                return BadRequest();
            }

            await _service.UpdateAsync(pago);
            await _service.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Pago/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(String id)
        {
            await _service.DeleteAsync(id);
            await _service.SaveChangesAsync();
            return NoContent();
        }
    }
}

