using Application.BrandonServices;
using Application.Dtos.PostDtos;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces.Brandon_Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinica_Dental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private readonly ISvFactura _svFactura;

        public FacturaController(ISvFactura svFactura)
        {
            _svFactura = svFactura;
        }

        // GET: api/<FacturaController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacturaDto>>> Get()
        {
            var facturas = await _svFactura.GetAllAsync();
            return Ok(facturas);
        }

        // GET api/<FacturaController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FacturaDto>> Get(string id)
        {
            var factura = await _svFactura.GetByIdAsync(id);
            if (factura == null)
            {
                return NotFound();
            }
            return Ok(factura);
        }

        // POST api/<FacturaController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] FacturaDto facturaDto)
        {
            if (facturaDto == null)
            {
                return BadRequest();
            }

            await _svFactura.AddAsync(facturaDto);
            await _svFactura.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = facturaDto.ID_Factura }, facturaDto);
        }

        // PUT api/<FacturaController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] FacturaDto facturaDto)
        {
            if (facturaDto == null || id != facturaDto.ID_Factura)
            {
                return BadRequest();
            }

            await _svFactura.UpdateAsync(facturaDto);
            await _svFactura.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/<FacturaController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var factura = await _svFactura.GetByIdAsync(id);
            if (factura == null)
            {
                return NotFound();
            }

            await _svFactura.DeleteAsync(id);
            await _svFactura.SaveChangesAsync();

            return NoContent();
        }
    }
}
