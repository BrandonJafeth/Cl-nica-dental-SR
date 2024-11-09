using Application.Dtos.PostDtos;
using Application.GenericService;
using Clinica_Dental;
using Domain.Interfaces.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinica_Dental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Estado_PagoController : ControllerBase
    {
        private readonly ISvGeneric<Estado_Pago> _service;

        public Estado_PagoController(ISvGeneric<Estado_Pago> service)
        {
            _service = service;
        }

        // GET: api/Estado_Pago
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadoPagoPostDto>>> Get()
        {
            var result = await _service.GetAllAsync();
            var dtoResult = result.Select(e => new EstadoPagoPostDto
            {
                ID_EstadoPago = e.ID_EstadoPago,
                Nombre_EP = e.Nombre_EP,
                Descripcion_EP = e.Descripcion_EP
            }).ToList();

            return Ok(dtoResult);
        }

        // GET: api/Estado_Pago/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EstadoPagoPostDto>> Get(string id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            var dtoResult = new EstadoPagoPostDto
            {
                ID_EstadoPago = result.ID_EstadoPago,
                Nombre_EP = result.Nombre_EP,
                Descripcion_EP = result.Descripcion_EP
            };

            return Ok(dtoResult);
        }

        // POST: api/Estado_Pago
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EstadoPagoPostDto estadoPagoDto)
        {
            var estadoPago = new Estado_Pago
            {
                ID_EstadoPago = estadoPagoDto.ID_EstadoPago,
                Nombre_EP = estadoPagoDto.Nombre_EP,
                Descripcion_EP = estadoPagoDto.Descripcion_EP
            };

            await _service.AddAsync(estadoPago);
            await _service.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = estadoPago.ID_EstadoPago }, estadoPagoDto);
        }

        // PUT: api/Estado_Pago/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] EstadoPagoPostDto estadoPagoDto)
        {
            if (id != estadoPagoDto.ID_EstadoPago)
            {
                return BadRequest();
            }

            var estadoPago = new Estado_Pago
            {
                ID_EstadoPago = estadoPagoDto.ID_EstadoPago,
                Nombre_EP = estadoPagoDto.Nombre_EP,
                Descripcion_EP = estadoPagoDto.Descripcion_EP
            };

            await _service.UpdateAsync(estadoPago);
            await _service.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Estado_Pago/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _service.DeleteAsync(id);
            await _service.SaveChangesAsync();
            return NoContent();
        }
    }
}
