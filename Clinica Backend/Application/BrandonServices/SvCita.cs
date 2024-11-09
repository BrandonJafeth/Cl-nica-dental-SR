using Application.Dtos.PostDtos;
using Clinica_Dental;
using Domain.Interfaces.Brandon_Interfaces;
using Domain.Interfaces.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BrandonServices
{
    public class SvCita : ISvCita
    {
        private readonly ISvGeneric<Cita> _genericRepository;

        public SvCita(ISvGeneric<Cita> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<IEnumerable<CitaDto>> GetAllAsync()
        {
            var citas = await _genericRepository.GetAllAsync();
            var citaDtos = new List<CitaDto>();

            foreach (var cita in citas)
            {
                citaDtos.Add(MapToDto(cita));
            }

            return citaDtos;
        }

        public async Task<CitaDto> GetByIdAsync(string id)
        {
            var cita = await _genericRepository.GetByIdAsync(id);
            return MapToDto(cita);
        }

        public async Task AddAsync(CitaDto entity)
        {
            var cita = MapToEntity(entity);
            await _genericRepository.AddAsync(cita);
        }

        public async Task UpdateAsync(CitaDto entity)
        {
            var cita = MapToEntity(entity);
            await _genericRepository.UpdateAsync(cita);
        }

        public async Task DeleteAsync(string id)
        {
            await _genericRepository.DeleteAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _genericRepository.SaveChangesAsync();
        }

        private CitaDto MapToDto(Cita cita)
        {
            return new CitaDto
            {
                ID_Cita = cita.ID_Cita,
                Fecha_Cita = (DateOnly)cita.Fecha_Cita,
                Motivo = cita.Motivo,
                Hora_Inicio = (TimeOnly)cita.Hora_Inicio,
                Hora_Fin = (TimeOnly)cita.Hora_Fin,
                ID_Paciente = cita.ID_Paciente,
                ID_Dentista = cita.ID_Dentista,
                ID_Funcionario = cita.ID_Funcionario,
                ID_EstadoCita = cita.ID_EstadoCita
            };
        }

        private Cita MapToEntity(CitaDto citaDto)
        {
            return new Cita
            {
                ID_Cita = citaDto.ID_Cita,
                Fecha_Cita = citaDto.Fecha_Cita,
                Motivo = citaDto.Motivo,
                Hora_Inicio = citaDto.Hora_Inicio,
                Hora_Fin = citaDto.Hora_Fin,
                ID_Paciente = citaDto.ID_Paciente,
                ID_Dentista = citaDto.ID_Dentista,
                ID_Funcionario = citaDto.ID_Funcionario,
                ID_EstadoCita = citaDto.ID_EstadoCita
            };
        }
    }
}
