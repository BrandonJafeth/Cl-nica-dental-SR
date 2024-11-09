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
    public class SvHistorialMedico : ISvHistorialMedico, ISvGeneric<HistorialMedicoDto>
    {
        private readonly MydDbContext _context;

        public SvHistorialMedico(MydDbContext context)
        {
            _context = context;
        }

        public async Task<HistorialMedicoDto> GetHistorialMedicoByIdAsync(string idHistorialMedico)
        {
            var historialMedico = await _context.Historial_Medicos.FindAsync(idHistorialMedico);

            if (historialMedico == null)
            {
                throw new Exception("Historial médico no encontrado");
            }

            return new HistorialMedicoDto
            {
                ID_HistorialMedico = historialMedico.ID_HistorialMedico,
                Fecha_Historial = historialMedico.Fecha_Historial.HasValue ? historialMedico.Fecha_Historial.Value.ToDateTime(TimeOnly.MinValue) : DateTime.MinValue,
                Diagnostico = historialMedico.Diagnostico,
                ID_Paciente = historialMedico.ID_Paciente
            };
        }

        public async Task<IEnumerable<HistorialMedicoDto>> GetAllHistorialesMedicosAsync()
        {
            var historialesMedicos = await _context.Historial_Medicos.ToListAsync();
            return historialesMedicos.Select(h => new HistorialMedicoDto
            {
                ID_HistorialMedico = h.ID_HistorialMedico,
                Fecha_Historial = h.Fecha_Historial.HasValue ? h.Fecha_Historial.Value.ToDateTime(TimeOnly.MinValue) : DateTime.MinValue,
                Diagnostico = h.Diagnostico,
                ID_Paciente = h.ID_Paciente
            });
        }

        public async Task RegisterHistorialMedicoAsync(HistorialMedicoDto historialMedicoDto)
        {
            var historialMedico = new Historial_Medico
            {
                ID_HistorialMedico = historialMedicoDto.ID_HistorialMedico,
                Fecha_Historial = DateOnly.FromDateTime(historialMedicoDto.Fecha_Historial),
                Diagnostico = historialMedicoDto.Diagnostico,
                ID_Paciente = historialMedicoDto.ID_Paciente
            };

            _context.Historial_Medicos.Add(historialMedico);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateHistorialMedicoAsync(HistorialMedicoDto historialMedicoDto)
        {
            var historialMedico = await _context.Historial_Medicos.FindAsync(historialMedicoDto.ID_HistorialMedico);

            if (historialMedico == null)
            {
                throw new Exception("Historial médico no encontrado");
            }

            historialMedico.Fecha_Historial = DateOnly.FromDateTime(historialMedicoDto.Fecha_Historial);
            historialMedico.Diagnostico = historialMedicoDto.Diagnostico;
            historialMedico.ID_Paciente = historialMedicoDto.ID_Paciente;

            _context.Historial_Medicos.Update(historialMedico);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteHistorialMedicoAsync(string idHistorialMedico)
        {
            var historialMedico = await _context.Historial_Medicos.FindAsync(idHistorialMedico);

            if (historialMedico == null)
            {
                throw new Exception("Historial médico no encontrado");
            }

            _context.Historial_Medicos.Remove(historialMedico);
            await _context.SaveChangesAsync();
        }

        // Implementación de ISvGeneric
        public async Task<IEnumerable<HistorialMedicoDto>> GetAllAsync()
        {
            return await GetAllHistorialesMedicosAsync();
        }

        public async Task<HistorialMedicoDto> GetByIdAsync(string id)
        {
            return await GetHistorialMedicoByIdAsync(id);
        }

        public async Task AddAsync(HistorialMedicoDto entity)
        {
            await RegisterHistorialMedicoAsync(entity);
        }

        public async Task UpdateAsync(HistorialMedicoDto entity)
        {
            await UpdateHistorialMedicoAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await DeleteHistorialMedicoAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
