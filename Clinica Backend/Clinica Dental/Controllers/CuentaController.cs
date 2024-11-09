using Application.BrandonServices;
using Application.Dtos.PostDtos;
using Domain.Interfaces.Brandon_Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinica_Dental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        private readonly ISvCuenta _svCuenta;

        public CuentaController(ISvCuenta svCuenta)
        {
            _svCuenta = svCuenta;
        }

        // GET: api/<CuentaController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CuentaDto>>> Get()
        {
            var cuentas = await _svCuenta.GetAllAsync();
            return Ok(cuentas);
        }

        // GET api/<CuentaController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CuentaDto>> Get(string id)
        {
            var cuenta = await _svCuenta.GetByIdAsync(id);
            if (cuenta == null)
            {
                return NotFound();
            }
            return Ok(cuenta);
        }

        // POST api/<CuentaController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CuentaDto cuentaDto)
        {
            if (cuentaDto == null)
            {
                return BadRequest();
            }

            await _svCuenta.AddAsync(cuentaDto);
            await _svCuenta.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = cuentaDto.ID_Cuenta }, cuentaDto);
        }

        // PUT api/<CuentaController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] CuentaDto cuentaDto)
        {
            if (cuentaDto == null || id != cuentaDto.ID_Cuenta)
            {
                return BadRequest();
            }

            await _svCuenta.UpdateAsync(cuentaDto);
            await _svCuenta.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/<CuentaController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var cuenta = await _svCuenta.GetByIdAsync(id);
            if (cuenta == null)
            {
                return NotFound();
            }

            await _svCuenta.DeleteAsync(id);
            await _svCuenta.SaveChangesAsync();

            return NoContent();
        }
    }
}

