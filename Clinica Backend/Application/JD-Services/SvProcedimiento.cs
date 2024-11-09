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
    public class SvProcedimiento : ISvProcedimiento, ISvGeneric<ProcedimientoDto>
    {
        private readonly MydDbContext _context;

        public SvProcedimiento(MydDbContext context)
        {
            _context = context;
        }

        public async Task<ProcedimientoDto> GetProcedimientoByIdAsync(string idProcedimiento)
        {
            var procedimiento = await _context.Procedimientos.FindAsync(idProcedimiento);

            if (procedimiento == null)
            {
                throw new Exception("Procedimiento no encontrado");
            }

            return new ProcedimientoDto
            {
                ID_Procedimiento = procedimiento.ID_Procedimiento,
                Fecha_Proc = procedimiento.Fecha_Proc.HasValue ? procedimiento.Fecha_Proc.Value.ToDateTime(TimeOnly.MinValue) : DateTime.MinValue,
                Detalles_Proc = procedimiento.Detalles_Proc,
                Hora_Inicio_Proc = procedimiento.Hora_Inicio_Proc.HasValue ? procedimiento.Hora_Inicio_Proc.Value.ToTimeSpan() : TimeSpan.Zero,
                Hora_Fin_Proc = procedimiento.Hora_Fin_Proc.HasValue ? procedimiento.Hora_Fin_Proc.Value.ToTimeSpan() : TimeSpan.Zero,
                ID_Tratamiento = procedimiento.ID_Tratamiento,
                ID_Paciente = procedimiento.ID_Paciente
            };
        }

        public async Task<IEnumerable<ProcedimientoDto>> GetAllProcedimientosAsync()
        {
            var procedimientos = await _context.Procedimientos.ToListAsync();
            return procedimientos.Select(p => new ProcedimientoDto
            {
                ID_Procedimiento = p.ID_Procedimiento,
                Fecha_Proc = p.Fecha_Proc.HasValue ? p.Fecha_Proc.Value.ToDateTime(TimeOnly.MinValue) : DateTime.MinValue,
                Detalles_Proc = p.Detalles_Proc,
                Hora_Inicio_Proc = p.Hora_Inicio_Proc.HasValue ? p.Hora_Inicio_Proc.Value.ToTimeSpan() : TimeSpan.Zero,
                Hora_Fin_Proc = p.Hora_Fin_Proc.HasValue ? p.Hora_Fin_Proc.Value.ToTimeSpan() : TimeSpan.Zero,
                ID_Tratamiento = p.ID_Tratamiento,
                ID_Paciente = p.ID_Paciente
            });
        }

        public async Task RegisterProcedimientoAsync(ProcedimientoDto procedimientoDto)
        {
            var procedimiento = new Procedimiento
            {
                ID_Procedimiento = procedimientoDto.ID_Procedimiento,
                Fecha_Proc = DateOnly.FromDateTime(procedimientoDto.Fecha_Proc),
                Detalles_Proc = procedimientoDto.Detalles_Proc,
                Hora_Inicio_Proc = TimeOnly.FromTimeSpan(procedimientoDto.Hora_Inicio_Proc),
                Hora_Fin_Proc = TimeOnly.FromTimeSpan(procedimientoDto.Hora_Fin_Proc),
                ID_Tratamiento = procedimientoDto.ID_Tratamiento,
                ID_Paciente = procedimientoDto.ID_Paciente
            };

            _context.Procedimientos.Add(procedimiento);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProcedimientoAsync(ProcedimientoDto procedimientoDto)
        {
            var procedimiento = await _context.Procedimientos.FindAsync(procedimientoDto.ID_Procedimiento);

            if (procedimiento == null)
            {
                throw new Exception("Procedimiento no encontrado");
            }

            procedimiento.Fecha_Proc = DateOnly.FromDateTime(procedimientoDto.Fecha_Proc);
            procedimiento.Detalles_Proc = procedimientoDto.Detalles_Proc;
            procedimiento.Hora_Inicio_Proc = TimeOnly.FromTimeSpan(procedimientoDto.Hora_Inicio_Proc);
            procedimiento.Hora_Fin_Proc = TimeOnly.FromTimeSpan(procedimientoDto.Hora_Fin_Proc);
            procedimiento.ID_Tratamiento = procedimientoDto.ID_Tratamiento;
            procedimiento.ID_Paciente = procedimientoDto.ID_Paciente;

            _context.Procedimientos.Update(procedimiento);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProcedimientoAsync(string idProcedimiento)
        {
            var procedimiento = await _context.Procedimientos.FindAsync(idProcedimiento);

            if (procedimiento == null)
            {
                throw new Exception("Procedimiento no encontrado");
            }

            _context.Procedimientos.Remove(procedimiento);
            await _context.SaveChangesAsync();
        }

        // Implementación de ISvGeneric
        public async Task<IEnumerable<ProcedimientoDto>> GetAllAsync()
        {
            return await GetAllProcedimientosAsync();
        }

        public async Task<ProcedimientoDto> GetByIdAsync(string id)
        {
            return await GetProcedimientoByIdAsync(id);
        }

        public async Task AddAsync(ProcedimientoDto entity)
        {
            await RegisterProcedimientoAsync(entity);
        }

        public async Task UpdateAsync(ProcedimientoDto entity)
        {
            await UpdateProcedimientoAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await DeleteProcedimientoAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
