using Application.Dtos.PostDtos;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacienteController : ControllerBase
    {
        private readonly SvPaciente _svPaciente;

        public PacienteController(SvPaciente svPaciente)
        {
            _svPaciente = svPaciente;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PacienteDto>> GetPacienteById(string id)
        {
            try
            {
                var paciente = await _svPaciente.GetPacienteByIdAsync(id);
                return Ok(paciente);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PacienteDto>>> GetAllPacientes()
        {
            var pacientes = await _svPaciente.GetAllPacientesAsync();
            return Ok(pacientes);
        }

        [HttpPost]
        public async Task<ActionResult> RegisterPaciente([FromBody] PacienteDto pacienteDto)
        {
            try
            {
                await _svPaciente.RegisterPacienteAsync(pacienteDto);
                return CreatedAtAction(nameof(GetPacienteById), new { id = pacienteDto.ID_Paciente }, pacienteDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePaciente(string id, [FromBody] PacienteDto pacienteDto)
        {
            if (id != pacienteDto.ID_Paciente)
            {
                return BadRequest("ID del paciente no coincide");
            }

            try
            {
                await _svPaciente.UpdatePacienteAsync(pacienteDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePaciente(string id)
        {
            try
            {
                await _svPaciente.DeletePacienteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
