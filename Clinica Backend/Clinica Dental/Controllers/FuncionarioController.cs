using Application.Dtos.PostDtos;
using Application.JD_Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinica_Dental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly SvFuncionario _svFuncionario;

        public FuncionarioController(SvFuncionario svFuncionario)
        {
            _svFuncionario = svFuncionario;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FuncionarioPostDto>> GetFuncionarioById(string id)
        {
            try
            {
                var funcionario = await _svFuncionario.GetFuncionarioByIdAsync(id);
                return Ok(funcionario);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FuncionarioPostDto>>> GetAllFuncionarios()
        {
            var funcionarios = await _svFuncionario.GetAllFuncionariosAsync();
            return Ok(funcionarios);
        }

        [HttpPost]
        public async Task<ActionResult> RegisterFuncionario([FromBody] FuncionarioPostDto funcionarioDto)
        {
            try
            {
                await _svFuncionario.RegisterFuncionarioAsync(funcionarioDto);
                return CreatedAtAction(nameof(GetFuncionarioById), new { id = funcionarioDto.ID_Funcionario }, funcionarioDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateFuncionario(string id, [FromBody] FuncionarioPostDto funcionarioDto)
        {
            if (id != funcionarioDto.ID_Funcionario)
            {
                return BadRequest("ID del funcionario no coincide");
            }

            try
            {
                await _svFuncionario.UpdateFuncionarioAsync(funcionarioDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFuncionario(string id)
        {
            try
            {
                await _svFuncionario.DeleteFuncionarioAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
