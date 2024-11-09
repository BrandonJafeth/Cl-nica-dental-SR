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
    public class ProcedimientosController : ControllerBase
    {
        private readonly ISvProcedimiento _svProcedimiento;

        public ProcedimientosController(ISvProcedimiento svProcedimiento)
        {
            _svProcedimiento = svProcedimiento;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProcedimientoDto>>> GetAllProcedimientos()
        {
            var procedimientos = await _svProcedimiento.GetAllProcedimientosAsync();
            return Ok(procedimientos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProcedimientoDto>> GetProcedimientoById(string id)
        {
            try
            {
                var procedimiento = await _svProcedimiento.GetProcedimientoByIdAsync(id);
                return Ok(procedimiento);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> RegisterProcedimiento([FromBody] ProcedimientoDto procedimientoDto)
        {
            try
            {
                await _svProcedimiento.RegisterProcedimientoAsync(procedimientoDto);
                return CreatedAtAction(nameof(GetProcedimientoById), new { id = procedimientoDto.ID_Procedimiento }, procedimientoDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProcedimiento(string id, [FromBody] ProcedimientoDto procedimientoDto)
        {
            if (id != procedimientoDto.ID_Procedimiento)
            {
                return BadRequest("ID del procedimiento no coincide");
            }

            try
            {
                await _svProcedimiento.UpdateProcedimientoAsync(procedimientoDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProcedimiento(string id)
        {
            try
            {
                await _svProcedimiento.DeleteProcedimientoAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
