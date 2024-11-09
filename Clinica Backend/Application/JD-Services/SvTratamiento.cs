using Application.Dtos.PostDtos;
using Clinica_Dental;
using Domain.Interfaces.Generic;
using Domain.Interfaces.JD_Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SvTratamiento : ISvTratamiento, ISvGeneric<TratamientoDto>
    {
        private readonly MydDbContext _context;

        public SvTratamiento(MydDbContext context)
        {
            _context = context;
        }

        public async Task<TratamientoDto> GetTratamientoByIdAsync(string idTratamiento)
        {
            var tratamiento = await _context.Tratamientos.FindAsync(idTratamiento);

            if (tratamiento == null)
            {
                throw new Exception("Tratamiento no encontrado");
            }

            return new TratamientoDto
            {
                ID_Tratamiento = tratamiento.ID_Tratamiento,
                Nombre_Tra = tratamiento.Nombre_Tra,
                Descripcion_Tra = tratamiento.Descripcion_Tra,
                ID_TipoTratamiento = tratamiento.ID_TipoTratamiento,
                ID_EstadoTratamiento = tratamiento.ID_EstadoTratamiento
            };
        }

        public async Task<IEnumerable<TratamientoDto>> GetAllTratamientosAsync()
        {
            var tratamientos = await _context.Tratamientos.ToListAsync();
            return tratamientos.Select(t => new TratamientoDto
            {
                ID_Tratamiento = t.ID_Tratamiento,
                Nombre_Tra = t.Nombre_Tra,
                Descripcion_Tra = t.Descripcion_Tra,
                ID_TipoTratamiento = t.ID_TipoTratamiento,
                ID_EstadoTratamiento = t.ID_EstadoTratamiento
            });
        }

        public async Task RegisterTratamientoAsync(TratamientoDto tratamientoDto)
        {
            var tratamiento = new Tratamiento
            {
                ID_Tratamiento = tratamientoDto.ID_Tratamiento,
                Nombre_Tra = tratamientoDto.Nombre_Tra,
                Descripcion_Tra = tratamientoDto.Descripcion_Tra,
                ID_TipoTratamiento = tratamientoDto.ID_TipoTratamiento,
                ID_EstadoTratamiento = tratamientoDto.ID_EstadoTratamiento
            };

            _context.Tratamientos.Add(tratamiento);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTratamientoAsync(TratamientoDto tratamientoDto)
        {
            var tratamiento = await _context.Tratamientos.FindAsync(tratamientoDto.ID_Tratamiento);

            if (tratamiento == null)
            {
                throw new Exception("Tratamiento no encontrado");
            }

            tratamiento.Nombre_Tra = tratamientoDto.Nombre_Tra;
            tratamiento.Descripcion_Tra = tratamientoDto.Descripcion_Tra;
            tratamiento.ID_TipoTratamiento = tratamientoDto.ID_TipoTratamiento;
            tratamiento.ID_EstadoTratamiento = tratamientoDto.ID_EstadoTratamiento;

            _context.Tratamientos.Update(tratamiento);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTratamientoAsync(string idTratamiento)
        {
            var tratamiento = await _context.Tratamientos.FindAsync(idTratamiento);

            if (tratamiento == null)
            {
                throw new Exception("Tratamiento no encontrado");
            }

            _context.Tratamientos.Remove(tratamiento);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TratamientoDto>> GetAllAsync()
        {
            return await GetAllTratamientosAsync();
        }

        public async Task<TratamientoDto> GetByIdAsync(string id)
        {
            return await GetTratamientoByIdAsync(id);
        }

        public async Task AddAsync(TratamientoDto entity)
        {
            await RegisterTratamientoAsync(entity);
        }

        public async Task UpdateAsync(TratamientoDto entity)
        {
            await UpdateTratamientoAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await DeleteTratamientoAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
