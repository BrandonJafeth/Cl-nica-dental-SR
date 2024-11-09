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
    public class Factura_ProcedimientoController : ControllerBase
    {
        private readonly ISvFactura_Procedimiento _svFacturaProcedimiento;

        public Factura_ProcedimientoController(ISvFactura_Procedimiento svFacturaProcedimiento)
        {
            _svFacturaProcedimiento = svFacturaProcedimiento;
        }

        // GET: api/<Factura_ProcedimientoController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacturaProcedimientoDto>>> Get()
        {
            var facturaProcedimientos = await _svFacturaProcedimiento.GetAllAsync();
            return Ok(facturaProcedimientos);
        }

        // GET api/<Factura_ProcedimientoController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FacturaProcedimientoDto>> Get(string id)
        {
            var facturaProcedimiento = await _svFacturaProcedimiento.GetByIdAsync(id);
            if (facturaProcedimiento == null)
            {
                return NotFound();
            }
            return Ok(facturaProcedimiento);
        }

        // POST api/<Factura_ProcedimientoController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] FacturaProcedimientoDto facturaProcedimientoDto)
        {
            if (facturaProcedimientoDto == null)
            {
                return BadRequest();
            }

            await _svFacturaProcedimiento.AddAsync(facturaProcedimientoDto);
            await _svFacturaProcedimiento.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = facturaProcedimientoDto.ID_Factura_Procedimiento }, facturaProcedimientoDto);
        }

        // PUT api/<Factura_ProcedimientoController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] FacturaProcedimientoDto facturaProcedimientoDto)
        {
            if (facturaProcedimientoDto == null || id != facturaProcedimientoDto.ID_Factura_Procedimiento)
            {
                return BadRequest();
            }

            await _svFacturaProcedimiento.UpdateAsync(facturaProcedimientoDto);
            await _svFacturaProcedimiento.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/<Factura_ProcedimientoController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var facturaProcedimiento = await _svFacturaProcedimiento.GetByIdAsync(id);
            if (facturaProcedimiento == null)
            {
                return NotFound();
            }

            await _svFacturaProcedimiento.DeleteAsync(id);
            await _svFacturaProcedimiento.SaveChangesAsync();

            return NoContent();
        }
    }
}

