using Application.Dtos.PostDtos;
using Domain.Interfaces.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.JD_Interfaces
{
    public interface ISvTratamiento : ISvGeneric<TratamientoDto>
    {
        Task<TratamientoDto> GetTratamientoByIdAsync(string idTratamiento);
        Task<IEnumerable<TratamientoDto>> GetAllTratamientosAsync();
        Task RegisterTratamientoAsync(TratamientoDto tratamientoDto);
        Task UpdateTratamientoAsync(TratamientoDto tratamientoDto);
        Task DeleteTratamientoAsync(string idTratamiento);
    }
}
