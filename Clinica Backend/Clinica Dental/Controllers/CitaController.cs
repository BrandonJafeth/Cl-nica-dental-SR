using Application.Dtos.PostDtos;
using Application.BrandonServices;
using Domain.Interfaces.Brandon_Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinica_Dental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitaController : ControllerBase
    {
        private readonly ISvCita _svCita;

        public CitaController(ISvCita svCita)
        {
            _svCita = svCita;
        }

        // GET: api/<CitaController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CitaDto>>> Get()
        {
            var citas = await _svCita.GetAllAsync();
            return Ok(citas);
        }

        // GET api/<CitaController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CitaDto>> Get(string id)
        {
            var cita = await _svCita.GetByIdAsync(id);
            if (cita == null)
            {
                return NotFound();
            }
            return Ok(cita);
        }

        // POST api/<CitaController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CitaDto citaDto)
        {
            if (citaDto == null)
            {
                return BadRequest();
            }

            await _svCita.AddAsync(citaDto);
            await _svCita.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = citaDto.ID_Cita }, citaDto);
        }

        // PUT api/<CitaController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] CitaDto citaDto)
        {
            if (citaDto == null || id != citaDto.ID_Cita)
            {
                return BadRequest();
            }

            await _svCita.UpdateAsync(citaDto);
            await _svCita.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/<CitaController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var cita = await _svCita.GetByIdAsync(id);
            if (cita == null)
            {
                return NotFound();
            }

            await _svCita.DeleteAsync(id);
            await _svCita.SaveChangesAsync();

            return NoContent();
        }
    }
}