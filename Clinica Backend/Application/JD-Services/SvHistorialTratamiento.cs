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
    public class SvHistorialTratamiento : ISvHistorialTratamiento, ISvGeneric<HistorialTratamientoDto>
    {
        private readonly MydDbContext _context;

        public SvHistorialTratamiento(MydDbContext context)
        {
            _context = context;
        }

        public async Task<HistorialTratamientoDto> GetHistorialTratamientoByIdAsync(string idHistorialTratamiento)
        {
            var historialTratamiento = await _context.Historial_Tratamientos.FindAsync(idHistorialTratamiento);

            if (historialTratamiento == null)
            {
                throw new Exception("Historial de tratamiento no encontrado");
            }

            return new HistorialTratamientoDto
            {
                ID_Historial_Tratamiento = historialTratamiento.ID_Historial_Tratamiento,
                ID_HistorialMedico = historialTratamiento.ID_HistorialMedico,
                ID_Tratamiento = historialTratamiento.ID_Tratamiento,
                Fecha_Tratamiento = historialTratamiento.Fecha_Tratamiento.HasValue ? historialTratamiento.Fecha_Tratamiento.Value.ToDateTime(TimeOnly.MinValue) : DateTime.MinValue
            };
        }

        public async Task<IEnumerable<HistorialTratamientoDto>> GetAllHistorialesTratamientosAsync()
        {
            var historialesTratamientos = await _context.Historial_Tratamientos.ToListAsync();
            return historialesTratamientos.Select(h => new HistorialTratamientoDto
            {
                ID_Historial_Tratamiento = h.ID_Historial_Tratamiento,
                ID_HistorialMedico = h.ID_HistorialMedico,
                ID_Tratamiento = h.ID_Tratamiento,
                Fecha_Tratamiento = h.Fecha_Tratamiento.HasValue ? h.Fecha_Tratamiento.Value.ToDateTime(TimeOnly.MinValue) : DateTime.MinValue
            });
        }

        public async Task RegisterHistorialTratamientoAsync(HistorialTratamientoDto historialTratamientoDto)
        {
            var historialTratamiento = new Historial_Tratamiento
            {
                ID_Historial_Tratamiento = historialTratamientoDto.ID_Historial_Tratamiento,
                ID_HistorialMedico = historialTratamientoDto.ID_HistorialMedico,
                ID_Tratamiento = historialTratamientoDto.ID_Tratamiento,
                Fecha_Tratamiento = DateOnly.FromDateTime(historialTratamientoDto.Fecha_Tratamiento)
            };

            _context.Historial_Tratamientos.Add(historialTratamiento);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateHistorialTratamientoAsync(HistorialTratamientoDto historialTratamientoDto)
        {
            var historialTratamiento = await _context.Historial_Tratamientos.FindAsync(historialTratamientoDto.ID_Historial_Tratamiento);

            if (historialTratamiento == null)
            {
                throw new Exception("Historial de tratamiento no encontrado");
            }

            historialTratamiento.ID_HistorialMedico = historialTratamientoDto.ID_HistorialMedico;
            historialTratamiento.ID_Tratamiento = historialTratamientoDto.ID_Tratamiento;
            historialTratamiento.Fecha_Tratamiento = DateOnly.FromDateTime(historialTratamientoDto.Fecha_Tratamiento);

            _context.Historial_Tratamientos.Update(historialTratamiento);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteHistorialTratamientoAsync(string idHistorialTratamiento)
        {
            var historialTratamiento = await _context.Historial_Tratamientos.FindAsync(idHistorialTratamiento);

            if (historialTratamiento == null)
            {
                throw new Exception("Historial de tratamiento no encontrado");
            }

            _context.Historial_Tratamientos.Remove(historialTratamiento);
            await _context.SaveChangesAsync();
        }

        // Implementación de ISvGeneric
        public async Task<IEnumerable<HistorialTratamientoDto>> GetAllAsync()
        {
            return await GetAllHistorialesTratamientosAsync();
        }

        public async Task<HistorialTratamientoDto> GetByIdAsync(string id)
        {
            return await GetHistorialTratamientoByIdAsync(id);
        }

        public async Task AddAsync(HistorialTratamientoDto entity)
        {
            await RegisterHistorialTratamientoAsync(entity);
        }

        public async Task UpdateAsync(HistorialTratamientoDto entity)
        {
            await UpdateHistorialTratamientoAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await DeleteHistorialTratamientoAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
