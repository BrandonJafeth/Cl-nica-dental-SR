using Application.Dtos.PostDtos;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagoController : ControllerBase
    {
        private readonly SvPago _svPago;

        public PagoController(SvPago svPago)
        {
            _svPago = svPago;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PagoDto>> GetPagoById(string id)
        {
            try
            {
                var pago = await _svPago.GetByIdAsync(id);
                return Ok(pago);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PagoDto>>> GetAllPagos()
        {
            var pagos = await _svPago.GetAllAsync();
            return Ok(pagos);
        }

        [HttpPost]
        public async Task<ActionResult> RegisterPago([FromBody] PagoDto pagoDto)
        {
            try
            {
                await _svPago.AddAsync(pagoDto);
                return CreatedAtAction(nameof(GetPagoById), new { id = pagoDto.ID_Pago }, pagoDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePago(string id, [FromBody] PagoDto pagoDto)
        {
            if (id != pagoDto.ID_Pago.ToString())
            {
                return BadRequest("ID del pago no coincide");
            }

            try
            {
                await _svPago.UpdateAsync(pagoDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePago(string id)
        {
            try
            {
                await _svPago.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
