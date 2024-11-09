using Application.Dtos.PostDtos;
using Domain.Interfaces.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.JD_Interfaces
{
    public interface ISvRole : ISvGeneric<RolesPostDto>
    {
        Task<RolesPostDto> GetRoleByIdAsync(string idRole);
        Task<IEnumerable<RolesPostDto>> GetAllRolesAsync();
        Task RegisterRoleAsync(RolesPostDto roleDto);
        Task UpdateRoleAsync(RolesPostDto roleDto);
        Task DeleteRoleAsync(string idRole);
    }
}
