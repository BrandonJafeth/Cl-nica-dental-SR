using Application.Dtos.PostDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISvUsuario
    {
        Task<UsuariosDto> LoginAsync(string email, string password);
        Task RegisterAsync(UsuariosDto usuario);
        Task<UsuariosDto> GetUserByIdAsync(string idUsuario);
        Task UpdateUserAsync(UsuariosDto usuario);
        Task DeleteUserAsync(string idUsuario);
        Task<IEnumerable<UsuariosDto>> GetAllUsersAsync(); 
    }
}