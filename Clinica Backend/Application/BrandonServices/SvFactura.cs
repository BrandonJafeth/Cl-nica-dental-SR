using Application.Dtos.PostDtos;
using Clinica_Dental;
using Domain.Interfaces.Brandon_Interfaces;
using Domain.Interfaces.Generic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.BrandonServices
{
    public class SvFactura : ISvFactura
    {
        private readonly ISvGeneric<Factura> _genericRepository;

        public SvFactura(ISvGeneric<Factura> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<IEnumerable<FacturaDto>> GetAllAsync()
        {
            var facturas = await _genericRepository.GetAllAsync();
            var facturaDtos = new List<FacturaDto>();

            foreach (var factura in facturas)
            {
                facturaDtos.Add(MapToDto(factura));
            }

            return facturaDtos;
        }

        public async Task<FacturaDto> GetByIdAsync(string id)
        {
            var factura = await _genericRepository.GetByIdAsync(id);
            return MapToDto(factura);
        }

        public async Task AddAsync(FacturaDto entity)
        {
            var factura = MapToEntity(entity);
            await _genericRepository.AddAsync(factura);
        }

        public async Task UpdateAsync(FacturaDto entity)
        {
            var factura = MapToEntity(entity);
            await _genericRepository.UpdateAsync(factura);
        }

        public async Task DeleteAsync(string id)
        {
            await _genericRepository.DeleteAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _genericRepository.SaveChangesAsync();
        }

        private FacturaDto MapToDto(Factura factura)
        {
            return new FacturaDto
            {
                ID_Factura = factura.ID_Factura,
                MontoTotal_Fa = (decimal)factura.MontoTotal_Fa,
                FechaEmision_Fa = factura.FechaEmision_Fa ?? DateOnly.MinValue,
                ID_EstadoPago = factura.ID_EstadoPago
            };
        }

        private Factura MapToEntity(FacturaDto facturaDto)
        {
            return new Factura
            {
                ID_Factura = facturaDto.ID_Factura,
                MontoTotal_Fa = facturaDto.MontoTotal_Fa,
                FechaEmision_Fa = facturaDto.FechaEmision_Fa,
                ID_EstadoPago = facturaDto.ID_EstadoPago
            };
        }
    }
}

