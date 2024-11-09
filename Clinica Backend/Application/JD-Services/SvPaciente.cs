using Application.Dtos.PostDtos;
using Clinica_Dental;
using Domain.Interfaces.JD_Interfaces;
using Domain.Interfaces.Generic;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SvPaciente : ISvPaciente, ISvGeneric<PacienteDto>
    {
        private readonly MydDbContext _context;

        public SvPaciente(MydDbContext context)
        {
            _context = context;
        }

        public async Task<PacienteDto> GetPacienteByIdAsync(string idPaciente)
        {
            var paciente = await _context.Pacientes.FindAsync(idPaciente);

            if (paciente == null)
            {
                throw new Exception("Paciente no encontrado");
            }

            return new PacienteDto
            {
                ID_Paciente = paciente.ID_Paciente,
                Nombre_Pac = paciente.Nombre_Pac,
                Apellido1_Pac = paciente.Apellido1_Pac,
                Apellido2_Pac = paciente.Apellido2_Pac,
                Fecha_Nacimiento_Pac = paciente.Fecha_Nacimiento_Pac.HasValue ? paciente.Fecha_Nacimiento_Pac.Value.ToDateTime(TimeOnly.MinValue) : DateTime.MinValue,
                Telefono_Pac = paciente.Telefono_Pac,
                Correo_Pac = paciente.Correo_Pac,
                Direccion_Pac = paciente.Direccion_Pac
            };
        }

        public async Task<IEnumerable<PacienteDto>> GetAllPacientesAsync()
        {
            var pacientes = await _context.Pacientes.ToListAsync();
            return pacientes.Select(p => new PacienteDto
            {
                ID_Paciente = p.ID_Paciente,
                Nombre_Pac = p.Nombre_Pac,
                Apellido1_Pac = p.Apellido1_Pac,
                Apellido2_Pac = p.Apellido2_Pac,
                Fecha_Nacimiento_Pac = p.Fecha_Nacimiento_Pac.HasValue ? p.Fecha_Nacimiento_Pac.Value.ToDateTime(TimeOnly.MinValue) : DateTime.MinValue,
                Telefono_Pac = p.Telefono_Pac,
                Correo_Pac = p.Correo_Pac,
                Direccion_Pac = p.Direccion_Pac
            });
        }

        public async Task RegisterPacienteAsync(PacienteDto pacienteDto)
        {
            var paciente = new Paciente
            {
                ID_Paciente = pacienteDto.ID_Paciente,
                Nombre_Pac = pacienteDto.Nombre_Pac,
                Apellido1_Pac = pacienteDto.Apellido1_Pac,
                Apellido2_Pac = pacienteDto.Apellido2_Pac,
                Fecha_Nacimiento_Pac = DateOnly.FromDateTime(pacienteDto.Fecha_Nacimiento_Pac),
                Telefono_Pac = pacienteDto.Telefono_Pac,
                Correo_Pac = pacienteDto.Correo_Pac,
                Direccion_Pac = pacienteDto.Direccion_Pac
            };

            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePacienteAsync(PacienteDto pacienteDto)
        {
            var paciente = await _context.Pacientes.FindAsync(pacienteDto.ID_Paciente);

            if (paciente == null)
            {
                throw new Exception("Paciente no encontrado");
            }

            paciente.Nombre_Pac = pacienteDto.Nombre_Pac;
            paciente.Apellido1_Pac = pacienteDto.Apellido1_Pac;
            paciente.Apellido2_Pac = pacienteDto.Apellido2_Pac;
            paciente.Fecha_Nacimiento_Pac = DateOnly.FromDateTime(pacienteDto.Fecha_Nacimiento_Pac);
            paciente.Telefono_Pac = pacienteDto.Telefono_Pac;
            paciente.Correo_Pac = pacienteDto.Correo_Pac;
            paciente.Direccion_Pac = pacienteDto.Direccion_Pac;

            _context.Pacientes.Update(paciente);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePacienteAsync(string idPaciente)
        {
            var paciente = await _context.Pacientes.FindAsync(idPaciente);

            if (paciente == null)
            {
                throw new Exception("Paciente no encontrado");
            }

            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();
        }

        // Implementación de ISvGeneric
        public async Task<IEnumerable<PacienteDto>> GetAllAsync()
        {
            return await GetAllPacientesAsync();
        }

        public async Task<PacienteDto> GetByIdAsync(string id)
        {
            return await GetPacienteByIdAsync(id);
        }

        public async Task AddAsync(PacienteDto entity)
        {
            await RegisterPacienteAsync(entity);
        }

        public async Task UpdateAsync(PacienteDto entity)
        {
            await UpdatePacienteAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await DeletePacienteAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

