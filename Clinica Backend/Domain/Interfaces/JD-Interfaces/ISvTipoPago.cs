using Application.Dtos.PostDtos;
using Domain.Interfaces.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.JD_Interfaces
{
    public interface ISvTipoPago : ISvGeneric<TipoPagoPostDto>
    {
        Task<TipoPagoPostDto> GetTipoPagoByIdAsync(string idTipoPago);
        Task<IEnumerable<TipoPagoPostDto>> GetAllTipoPagosAsync();
        Task RegisterTipoPagoAsync(TipoPagoPostDto tipoPagoDto);
        Task UpdateTipoPagoAsync(TipoPagoPostDto tipoPagoDto);
        Task DeleteTipoPagoAsync(string idTipoPago);
    }
}
