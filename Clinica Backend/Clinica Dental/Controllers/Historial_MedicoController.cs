using Application.Dtos.PostDtos;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialMedicoController : ControllerBase
    {
        private readonly SvHistorialMedico _svHistorialMedico;

        public HistorialMedicoController(SvHistorialMedico svHistorialMedico)
        {
            _svHistorialMedico = svHistorialMedico;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistorialMedicoDto>>> GetAllHistorialesMedicos()
        {
            var historialesMedicos = await _svHistorialMedico.GetAllHistorialesMedicosAsync();
            return Ok(historialesMedicos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HistorialMedicoDto>> GetHistorialMedicoById(string id)
        {
            var historialMedico = await _svHistorialMedico.GetHistorialMedicoByIdAsync(id);

            if (historialMedico == null)
            {
                return NotFound();
            }

            return Ok(historialMedico);
        }

        [HttpPost]
        public async Task<ActionResult> RegisterHistorialMedico([FromBody] HistorialMedicoDto historialMedicoDto)
        {
            await _svHistorialMedico.RegisterHistorialMedicoAsync(historialMedicoDto);
            return CreatedAtAction(nameof(GetHistorialMedicoById), new { id = historialMedicoDto.ID_HistorialMedico }, historialMedicoDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateHistorialMedico(string id, [FromBody] HistorialMedicoDto historialMedicoDto)
        {
            if (id != historialMedicoDto.ID_HistorialMedico)
            {
                return BadRequest();
            }

            await _svHistorialMedico.UpdateHistorialMedicoAsync(historialMedicoDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteHistorialMedico(string id)
        {
            await _svHistorialMedico.DeleteHistorialMedicoAsync(id);
            return NoContent();
        }
    }
}
