using Application.Dtos.PostDtos;
using Clinica_Dental;
using Domain.Interfaces.Brandon_Interfaces;
using Domain.Interfaces.Generic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.BrandonServices
{
    public class SvFactura_Tratamiento : ISvFactura_Tratamiento
    {
        private readonly ISvGeneric<Factura_Tratamiento> _genericRepository;

        public SvFactura_Tratamiento(ISvGeneric<Factura_Tratamiento> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<IEnumerable<FacturaTratamientoDto>> GetAllAsync()
        {
            var facturaTratamientos = await _genericRepository.GetAllAsync();
            var facturaTratamientoDtos = new List<FacturaTratamientoDto>();

            foreach (var facturaTratamiento in facturaTratamientos)
            {
                facturaTratamientoDtos.Add(MapToDto(facturaTratamiento));
            }

            return facturaTratamientoDtos;
        }

        public async Task<FacturaTratamientoDto> GetByIdAsync(string id)
        {
            var facturaTratamiento = await _genericRepository.GetByIdAsync(id);
            return MapToDto(facturaTratamiento);
        }

        public async Task AddAsync(FacturaTratamientoDto entity)
        {
            var facturaTratamiento = MapToEntity(entity);
            await _genericRepository.AddAsync(facturaTratamiento);
        }

        public async Task UpdateAsync(FacturaTratamientoDto entity)
        {
            var facturaTratamiento = MapToEntity(entity);
            await _genericRepository.UpdateAsync(facturaTratamiento);
        }

        public async Task DeleteAsync(string id)
        {
            await _genericRepository.DeleteAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _genericRepository.SaveChangesAsync();
        }

        private FacturaTratamientoDto MapToDto(Factura_Tratamiento facturaTratamiento)
        {
            return new FacturaTratamientoDto
            {
                ID_Factura_Tratamiento = facturaTratamiento.ID_Factura_Tratamiento,
                ID_Factura = facturaTratamiento.ID_Factura,
                ID_Tratamiento = facturaTratamiento.ID_Tratamiento
            };
        }

        private Factura_Tratamiento MapToEntity(FacturaTratamientoDto facturaTratamientoDto)
        {
            return new Factura_Tratamiento
            {
                ID_Factura_Tratamiento = facturaTratamientoDto.ID_Factura_Tratamiento,
                ID_Factura = facturaTratamientoDto.ID_Factura,
                ID_Tratamiento = facturaTratamientoDto.ID_Tratamiento
            };
        }
    }
}
