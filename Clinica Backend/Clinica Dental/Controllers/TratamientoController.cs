using Application.Dtos.PostDtos;
using Domain.Interfaces.JD_Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TratamientoController : ControllerBase
    {
        private readonly ISvTratamiento _svTratamiento;

        public TratamientoController(ISvTratamiento svTratamiento)
        {
            _svTratamiento = svTratamiento;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TratamientoDto>>> GetAllTratamientos()
        {
            var tratamientos = await _svTratamiento.GetAllTratamientosAsync();
            return Ok(tratamientos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TratamientoDto>> GetTratamientoById(string id)
        {
            var tratamiento = await _svTratamiento.GetTratamientoByIdAsync(id);

            if (tratamiento == null)
            {
                return NotFound();
            }

            return Ok(tratamiento);
        }

        [HttpPost]
        public async Task<ActionResult> RegisterTratamiento([FromBody] TratamientoDto tratamientoDto)
        {
            await _svTratamiento.RegisterTratamientoAsync(tratamientoDto);
            return CreatedAtAction(nameof(GetTratamientoById), new { id = tratamientoDto.ID_Tratamiento }, tratamientoDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTratamiento(string id, [FromBody] TratamientoDto tratamientoDto)
        {
            if (id != tratamientoDto.ID_Tratamiento)
            {
                return BadRequest();
            }

            await _svTratamiento.UpdateTratamientoAsync(tratamientoDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTratamiento(string id)
        {
            await _svTratamiento.DeleteTratamientoAsync(id);
            return NoContent();
        }
    }
}
