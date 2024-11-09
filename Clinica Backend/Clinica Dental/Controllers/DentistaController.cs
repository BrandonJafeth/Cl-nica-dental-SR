using Application.BrandonServices;
using Application.Dtos.PostDtos;
using Domain.Interfaces.Brandon_Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinica_Dental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DentistaController : ControllerBase
    {
        private readonly ISvDentista _svDentista;

        public DentistaController(ISvDentista svDentista)
        {
            _svDentista = svDentista;
        }

        // GET: api/<DentistaController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DentistaDto>>> Get()
        {
            var dentistas = await _svDentista.GetAllAsync();
            return Ok(dentistas);
        }

        // GET api/<DentistaController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DentistaDto>> Get(string id)
        {
            var dentista = await _svDentista.GetByIdAsync(id);
            if (dentista == null)
            {
                return NotFound();
            }
            return Ok(dentista);
        }

        // POST api/<DentistaController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DentistaDto dentistaDto)
        {
            if (dentistaDto == null)
            {
                return BadRequest();
            }

            await _svDentista.AddAsync(dentistaDto);
            await _svDentista.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = dentistaDto.ID_Dentista }, dentistaDto);
        }

        // PUT api/<DentistaController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] DentistaDto dentistaDto)
        {
            if (dentistaDto == null || id != dentistaDto.ID_Dentista)
            {
                return BadRequest();
            }

            await _svDentista.UpdateAsync(dentistaDto);
            await _svDentista.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/<DentistaController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var dentista = await _svDentista.GetByIdAsync(id);
            if (dentista == null)
            {
                return NotFound();
            }

            await _svDentista.DeleteAsync(id);
            await _svDentista.SaveChangesAsync();

            return NoContent();
        }
    }
}
