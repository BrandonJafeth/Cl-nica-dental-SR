using Application.Dtos.PostDtos;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialTratamientoController : ControllerBase
    {
        private readonly SvHistorialTratamiento _svHistorialTratamiento;

        public HistorialTratamientoController(SvHistorialTratamiento svHistorialTratamiento)
        {
            _svHistorialTratamiento = svHistorialTratamiento;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistorialTratamientoDto>>> GetAllHistorialesTratamientos()
        {
            var historialesTratamientos = await _svHistorialTratamiento.GetAllHistorialesTratamientosAsync();
            return Ok(historialesTratamientos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HistorialTratamientoDto>> GetHistorialTratamientoById(string id)
        {
            var historialTratamiento = await _svHistorialTratamiento.GetHistorialTratamientoByIdAsync(id);

            if (historialTratamiento == null)
            {
                return NotFound();
            }

            return Ok(historialTratamiento);
        }

        [HttpPost]
        public async Task<ActionResult> RegisterHistorialTratamiento([FromBody] HistorialTratamientoDto historialTratamientoDto)
        {
            await _svHistorialTratamiento.RegisterHistorialTratamientoAsync(historialTratamientoDto);
            return CreatedAtAction(nameof(GetHistorialTratamientoById), new { id = historialTratamientoDto.ID_Historial_Tratamiento }, historialTratamientoDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateHistorialTratamiento(string id, [FromBody] HistorialTratamientoDto historialTratamientoDto)
        {
            if (id != historialTratamientoDto.ID_Historial_Tratamiento)
            {
                return BadRequest();
            }

            await _svHistorialTratamiento.UpdateHistorialTratamientoAsync(historialTratamientoDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteHistorialTratamiento(string id)
        {
            await _svHistorialTratamiento.DeleteHistorialTratamientoAsync(id);
            return NoContent();
        }
    }
}
