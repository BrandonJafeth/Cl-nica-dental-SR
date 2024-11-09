using Application.Dtos.PostDtos;
using Clinica_Dental;
using Domain.Interfaces;
using Domain.Interfaces.JD_Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.JD_Services
{
    public class SvFuncionario : ISvFuncionario
    {
        private readonly MydDbContext _context;

        public SvFuncionario(MydDbContext context)
        {
            _context = context;
        }

        public async Task<FuncionarioPostDto> GetFuncionarioByIdAsync(string idFuncionario)
        {
            var funcionario = await _context.Funcionarios.FindAsync(idFuncionario);

            if (funcionario == null)
            {
                throw new Exception("Funcionario no encontrado");
            }

            return new FuncionarioPostDto
            {
                ID_Funcionario = funcionario.ID_Funcionario,
                Nombre = funcionario.Nombre,
                Apellido1 = funcionario.Apellido1,
                Apellido2 = funcionario.Apellido2,
                Email = funcionario.Email,
                Contraseña = funcionario.Contraseña
            };
        }

        public async Task<IEnumerable<FuncionarioPostDto>> GetAllFuncionariosAsync()
        {
            var funcionarios = await _context.Funcionarios.ToListAsync();
            return funcionarios.Select(f => new FuncionarioPostDto
            {
                ID_Funcionario = f.ID_Funcionario,
                Nombre = f.Nombre,
                Apellido1 = f.Apellido1,
                Apellido2 = f.Apellido2,
                Email = f.Email,
                Contraseña = f.Contraseña
            });
        }

        public async Task RegisterFuncionarioAsync(FuncionarioPostDto funcionarioDto)
        {
            var funcionario = new Funcionario
            {
                ID_Funcionario = funcionarioDto.ID_Funcionario,
                Nombre = funcionarioDto.Nombre,
                Apellido1 = funcionarioDto.Apellido1,
                Apellido2 = funcionarioDto.Apellido2,
                Email = funcionarioDto.Email,
                Contraseña = funcionarioDto.Contraseña
            };

            _context.Funcionarios.Add(funcionario);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFuncionarioAsync(FuncionarioPostDto funcionarioDto)
        {
            var funcionario = await _context.Funcionarios.FindAsync(funcionarioDto.ID_Funcionario);

            if (funcionario == null)
            {
                throw new Exception("Funcionario no encontrado");
            }

            funcionario.Nombre = funcionarioDto.Nombre;
            funcionario.Apellido1 = funcionarioDto.Apellido1;
            funcionario.Apellido2 = funcionarioDto.Apellido2;
            funcionario.Email = funcionarioDto.Email;
            funcionario.Contraseña = funcionarioDto.Contraseña;

            _context.Funcionarios.Update(funcionario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFuncionarioAsync(string idFuncionario)
        {
            var funcionario = await _context.Funcionarios.FindAsync(idFuncionario);

            if (funcionario == null)
            {
                throw new Exception("Funcionario no encontrado");
            }

            _context.Funcionarios.Remove(funcionario);
            await _context.SaveChangesAsync();
        }
    }
}

