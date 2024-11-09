using Application.Dtos.PostDtos;
using Clinica_Dental;
using Domain.Interfaces.Brandon_Interfaces;
using Domain.Interfaces.Generic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.BrandonServices
{
    public class SvFactura_Procedimiento : ISvFactura_Procedimiento
    {
        private readonly ISvGeneric<Factura_Procedimiento> _genericRepository;

        public SvFactura_Procedimiento(ISvGeneric<Factura_Procedimiento> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<IEnumerable<FacturaProcedimientoDto>> GetAllAsync()
        {
            var facturaProcedimientos = await _genericRepository.GetAllAsync();
            var facturaProcedimientoDtos = new List<FacturaProcedimientoDto>();

            foreach (var facturaProcedimiento in facturaProcedimientos)
            {
                facturaProcedimientoDtos.Add(MapToDto(facturaProcedimiento));
            }

            return facturaProcedimientoDtos;
        }

        public async Task<FacturaProcedimientoDto> GetByIdAsync(string id)
        {
            var facturaProcedimiento = await _genericRepository.GetByIdAsync(id);
            return MapToDto(facturaProcedimiento);
        }

        public async Task AddAsync(FacturaProcedimientoDto entity)
        {
            var facturaProcedimiento = MapToEntity(entity);
            await _genericRepository.AddAsync(facturaProcedimiento);
        }

        public async Task UpdateAsync(FacturaProcedimientoDto entity)
        {
            var facturaProcedimiento = MapToEntity(entity);
            await _genericRepository.UpdateAsync(facturaProcedimiento);
        }

        public async Task DeleteAsync(string id)
        {
            await _genericRepository.DeleteAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _genericRepository.SaveChangesAsync();
        }

        private FacturaProcedimientoDto MapToDto(Factura_Procedimiento facturaProcedimiento)
        {
            return new FacturaProcedimientoDto
            {
                ID_Factura_Procedimiento = facturaProcedimiento.ID_Factura_Procedimiento,
                ID_Factura = facturaProcedimiento.ID_Factura,
                ID_Procedimiento = facturaProcedimiento.ID_Procedimiento
            };
        }

        private Factura_Procedimiento MapToEntity(FacturaProcedimientoDto facturaProcedimientoDto)
        {
            return new Factura_Procedimiento
            {
                ID_Factura_Procedimiento = facturaProcedimientoDto.ID_Factura_Procedimiento,
                ID_Factura = facturaProcedimientoDto.ID_Factura,
                ID_Procedimiento = facturaProcedimientoDto.ID_Procedimiento
            };
        }
    }
}


