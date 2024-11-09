using Application.Dtos.PostDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.JD_Interfaces
{
    public interface ISvFuncionario
    {
        Task<FuncionarioPostDto> GetFuncionarioByIdAsync(string idFuncionario);
        Task<IEnumerable<FuncionarioPostDto>> GetAllFuncionariosAsync();
        Task RegisterFuncionarioAsync(FuncionarioPostDto funcionarioDto);
        Task UpdateFuncionarioAsync(FuncionarioPostDto funcionarioDto);
        Task DeleteFuncionarioAsync(string idFuncionario);
    }
}
