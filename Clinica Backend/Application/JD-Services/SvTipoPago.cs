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
    public class SvTipoPago : ISvTipoPago, ISvGeneric<TipoPagoPostDto>
    {
        private readonly MydDbContext _context;

        public SvTipoPago(MydDbContext context)
        {
            _context = context;
        }

        public async Task<TipoPagoPostDto> GetTipoPagoByIdAsync(string idTipoPago)
        {
            var tipoPago = await _context.Tipo_Pagos.FindAsync(idTipoPago);

            if (tipoPago == null)
            {
                throw new Exception("Tipo de pago no encontrado");
            }

            return new TipoPagoPostDto
            {
                ID_Tipo_Pago = tipoPago.ID_Tipo_Pago,
                Nombre_TP = tipoPago.Nombre_TP,
                Descripcion_TP = tipoPago.Descripcion_TP
            };
        }

        public async Task<IEnumerable<TipoPagoPostDto>> GetAllTipoPagosAsync()
        {
            var tipoPagos = await _context.Tipo_Pagos.ToListAsync();
            return tipoPagos.Select(tp => new TipoPagoPostDto
            {
                ID_Tipo_Pago = tp.ID_Tipo_Pago,
                Nombre_TP = tp.Nombre_TP,
                Descripcion_TP = tp.Descripcion_TP
            });
        }

        public async Task RegisterTipoPagoAsync(TipoPagoPostDto tipoPagoDto)
        {
            var tipoPago = new Tipo_Pago
            {
                ID_Tipo_Pago = tipoPagoDto.ID_Tipo_Pago,
                Nombre_TP = tipoPagoDto.Nombre_TP,
                Descripcion_TP = tipoPagoDto.Descripcion_TP
            };

            _context.Tipo_Pagos.Add(tipoPago);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTipoPagoAsync(TipoPagoPostDto tipoPagoDto)
        {
            var tipoPago = await _context.Tipo_Pagos.FindAsync(tipoPagoDto.ID_Tipo_Pago);

            if (tipoPago == null)
            {
                throw new Exception("Tipo de pago no encontrado");
            }

            tipoPago.Nombre_TP = tipoPagoDto.Nombre_TP;
            tipoPago.Descripcion_TP = tipoPagoDto.Descripcion_TP;

            _context.Tipo_Pagos.Update(tipoPago);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTipoPagoAsync(string idTipoPago)
        {
            var tipoPago = await _context.Tipo_Pagos.FindAsync(idTipoPago);

            if (tipoPago == null)
            {
                throw new Exception("Tipo de pago no encontrado");
            }

            _context.Tipo_Pagos.Remove(tipoPago);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TipoPagoPostDto>> GetAllAsync()
        {
            return await GetAllTipoPagosAsync();
        }

        public async Task<TipoPagoPostDto> GetByIdAsync(string id)
        {
            return await GetTipoPagoByIdAsync(id);
        }

        public async Task AddAsync(TipoPagoPostDto entity)
        {
            await RegisterTipoPagoAsync(entity);
        }

        public async Task UpdateAsync(TipoPagoPostDto entity)
        {
            await UpdateTipoPagoAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await DeleteTipoPagoAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
