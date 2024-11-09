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
    public class TipoPagoController : ControllerBase
    {
        private readonly ISvTipoPago _svTipoPago;

        public TipoPagoController(ISvTipoPago svTipoPago)
        {
            _svTipoPago = svTipoPago;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoPagoPostDto>> GetTipoPagoById(string id)
        {
            var tipoPago = await _svTipoPago.GetTipoPagoByIdAsync(id);

            if (tipoPago == null)
            {
                return NotFound();
            }

            return Ok(tipoPago);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoPagoPostDto>>> GetAllTipoPagos()
        {
            var tipoPagos = await _svTipoPago.GetAllTipoPagosAsync();
            return Ok(tipoPagos);
        }

        [HttpPost]
        public async Task<ActionResult> RegisterTipoPago(TipoPagoPostDto tipoPagoDto)
        {
            await _svTipoPago.RegisterTipoPagoAsync(tipoPagoDto);
            return CreatedAtAction(nameof(GetTipoPagoById), new { id = tipoPagoDto.ID_Tipo_Pago }, tipoPagoDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTipoPago(string id, TipoPagoPostDto tipoPagoDto)
        {
            if (id != tipoPagoDto.ID_Tipo_Pago)
            {
                return BadRequest();
            }

            await _svTipoPago.UpdateTipoPagoAsync(tipoPagoDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTipoPago(string id)
        {
            await _svTipoPago.DeleteTipoPagoAsync(id);
            return NoContent();
        }
    }
}
