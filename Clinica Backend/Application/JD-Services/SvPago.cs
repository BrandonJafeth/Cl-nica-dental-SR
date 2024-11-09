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
    public class SvPago : ISvPago, ISvGeneric<PagoDto>
    {
        private readonly MydDbContext _context;

        public SvPago(MydDbContext context)
        {
            _context = context;
        }

        public async Task<PagoDto> GetPagoByIdAsync(Guid idPago)
        {
            var pago = await _context.Pagos.FindAsync(idPago);

            if (pago == null)
            {
                throw new Exception("Pago no encontrado");
            }

            return new PagoDto
            {
                ID_Pago = pago.ID_Pago,
                Monto_Pago = pago.Monto_Pago ?? 0,
                Fecha_Pago = pago.Fecha_Pago.HasValue ? pago.Fecha_Pago.Value.ToDateTime(TimeOnly.MinValue) : DateTime.MinValue,
                ID_Factura = pago.ID_Factura,
                ID_Tipo_Pago = pago.ID_Tipo_Pago
            };
        }

        public async Task<IEnumerable<PagoDto>> GetAllPagosAsync()
        {
            var pagos = await _context.Pagos.ToListAsync();
            return pagos.Select(p => new PagoDto
            {
                ID_Pago = p.ID_Pago,
                Monto_Pago = p.Monto_Pago ?? 0,
                Fecha_Pago = p.Fecha_Pago.HasValue ? p.Fecha_Pago.Value.ToDateTime(TimeOnly.MinValue) : DateTime.MinValue,
                ID_Factura = p.ID_Factura,
                ID_Tipo_Pago = p.ID_Tipo_Pago
            });
        }

        public async Task RegisterPagoAsync(PagoDto pagoDto)
        {
            var pago = new Pago
            {
                ID_Pago = pagoDto.ID_Pago,
                Monto_Pago = pagoDto.Monto_Pago,
                Fecha_Pago = DateOnly.FromDateTime(pagoDto.Fecha_Pago),
                ID_Factura = pagoDto.ID_Factura,
                ID_Tipo_Pago = pagoDto.ID_Tipo_Pago
            };

            _context.Pagos.Add(pago);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePagoAsync(PagoDto pagoDto)
        {
            var pago = await _context.Pagos.FindAsync(pagoDto.ID_Pago);

            if (pago == null)
            {
                throw new Exception("Pago no encontrado");
            }

            pago.Monto_Pago = pagoDto.Monto_Pago;
            pago.Fecha_Pago = DateOnly.FromDateTime(pagoDto.Fecha_Pago);
            pago.ID_Factura = pagoDto.ID_Factura;
            pago.ID_Tipo_Pago = pagoDto.ID_Tipo_Pago;

            _context.Pagos.Update(pago);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePagoAsync(Guid idPago)
        {
            var pago = await _context.Pagos.FindAsync(idPago);

            if (pago == null)
            {
                throw new Exception("Pago no encontrado");
            }

            _context.Pagos.Remove(pago);
            await _context.SaveChangesAsync();
        }

        // Implementación de ISvGeneric
        public async Task<IEnumerable<PagoDto>> GetAllAsync()
        {
            return await GetAllPagosAsync();
        }

        public async Task<PagoDto> GetByIdAsync(string id)
        {
            if (!Guid.TryParse(id, out var guid))
            {
                throw new ArgumentException("ID inválido");
            }
            return await GetPagoByIdAsync(guid);
        }

        public async Task AddAsync(PagoDto entity)
        {
            await RegisterPagoAsync(entity);
        }

        public async Task UpdateAsync(PagoDto entity)
        {
            await UpdatePagoAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            if (!Guid.TryParse(id, out var guid))
            {
                throw new ArgumentException("ID inválido");
            }
            await DeletePagoAsync(guid);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
