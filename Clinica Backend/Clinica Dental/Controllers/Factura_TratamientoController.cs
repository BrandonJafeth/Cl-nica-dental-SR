using Application.Dtos.PostDtos;
using Domain.Interfaces.Brandon_Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinica_Dental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Factura_TratamientoController : ControllerBase
    {
        private readonly ISvFactura_Tratamiento _service;

        public Factura_TratamientoController(ISvFactura_Tratamiento service)
        {
            _service = service;
        }

        // GET: api/Factura_Tratamiento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacturaTratamientoDto>>> Get()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        // GET: api/Factura_Tratamiento/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FacturaTratamientoDto>> Get(string id)
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
        public async Task<ActionResult> Post([FromBody] FacturaTratamientoDto facturaTratamientoDto)
        {
            if (facturaTratamientoDto == null)
            {
                return BadRequest();
            }

            await _service.AddAsync(facturaTratamientoDto);
            await _service.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = facturaTratamientoDto.ID_Factura_Tratamiento }, facturaTratamientoDto);
        }

        // PUT: api/Factura_Tratamiento/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] FacturaTratamientoDto facturaTratamientoDto)
        {
            if (id != facturaTratamientoDto.ID_Factura_Tratamiento)
            {
                return BadRequest();
            }

            await _service.UpdateAsync(facturaTratamientoDto);
            await _service.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Factura_Tratamiento/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            await _service.DeleteAsync(id);
            await _service.SaveChangesAsync();
            return NoContent();
        }
    }
}
