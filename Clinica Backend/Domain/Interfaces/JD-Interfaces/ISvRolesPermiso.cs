using Application.Dtos.PostDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.JD_Interfaces
{
    public interface ISvRolesPermiso
    {
        Task<RolesPermisosPostDto> GetRolesPermisoByIdAsync(string idRolesPermiso);
        Task<IEnumerable<RolesPermisosPostDto>> GetAllRolesPermisosAsync();
        Task RegisterRolesPermisoAsync(RolesPermisosPostDto rolesPermisosDto);
        Task UpdateRolesPermisoAsync(RolesPermisosPostDto rolesPermisosDto);
        Task DeleteRolesPermisoAsync(string idRolesPermiso);
    }
}
