using Application.Dtos.PostDtos;
using Application.Services;
using Domain.Interfaces.JD_Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoTratamientoController : ControllerBase
    {
        private readonly ISvTipoTratamiento _svTipoTratamiento;

        public TipoTratamientoController(ISvTipoTratamiento svTipoTratamiento)
        {
            _svTipoTratamiento = svTipoTratamiento;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoTratamientoPostDto>> GetTipoTratamientoById(string id)
        {
            var tipoTratamiento = await _svTipoTratamiento.GetTipoTratamientoByIdAsync(id);

            if (tipoTratamiento == null)
            {
                return NotFound();
            }

            return Ok(tipoTratamiento);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoTratamientoPostDto>>> GetAllTipoTratamientos()
        {
            var tipoTratamientos = await _svTipoTratamiento.GetAllTipoTratamientosAsync();
            return Ok(tipoTratamientos);
        }

        [HttpPost]
        public async Task<ActionResult> RegisterTipoTratamiento(TipoTratamientoPostDto tipoTratamientoDto)
        {
            await _svTipoTratamiento.RegisterTipoTratamientoAsync(tipoTratamientoDto);
            return CreatedAtAction(nameof(GetTipoTratamientoById), new { id = tipoTratamientoDto.ID_TipoTratamiento }, tipoTratamientoDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTipoTratamiento(string id, TipoTratamientoPostDto tipoTratamientoDto)
        {
            if (id != tipoTratamientoDto.ID_TipoTratamiento)
            {
                return BadRequest();
            }

            await _svTipoTratamiento.UpdateTipoTratamientoAsync(tipoTratamientoDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTipoTratamiento(string id)
        {
            await _svTipoTratamiento.DeleteTipoTratamientoAsync(id);
            return NoContent();
        }
    }
}
